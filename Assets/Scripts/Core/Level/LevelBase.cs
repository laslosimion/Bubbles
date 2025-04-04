using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class LevelBase : MonoBehaviour, IRuntimeInitializable
{
    private const float CameraMovementDuration = 3f;
    
    public event Action OnLevelCompleted;
    
    [SerializeField] protected LevelInfo _levelInfo;
    [SerializeField] protected SubLevelBase[] _subLevels;

    protected int _currentSublevel;
    
    private bool _canSpawnBubbles;
    private readonly List<Bubble> _spawnedBubbles = new();
    private Bubble _currentBubble;
    
    private Camera _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
        
        _mainCamera.transform.position = new Vector3(0, 0, _mainCamera.transform.position.z);
    }

    public virtual void Initialize()
    {
        _canSpawnBubbles = true;

        Main.Instance.ScoreUIHandler.IncreaseMoves(_levelInfo.subLevels[_currentSublevel].moves);
        _subLevels[_currentSublevel].IncreasePoints(_levelInfo.subLevels[_currentSublevel].points, true);

        if (_mainCamera == null)
        {
            Debug.LogError(GetType() + "Can not find main camera!");
            return;
        }
        
        _mainCamera.orthographicSize = _levelInfo.subLevels[_currentSublevel].cameraSize;
        _mainCamera.transform.DOMoveY(_levelInfo.subLevels[_currentSublevel].cameraY, CameraMovementDuration).SetEase(Ease.Linear);
        
        _subLevels[_currentSublevel].Initialize();
    }
    
    protected virtual void OnDestroy()
    {
        RemoveBubblesListeners();

        foreach (var item in _spawnedBubbles)
        {
            Destroy(item.gameObject);
        }
        _spawnedBubbles.Clear();

        _canSpawnBubbles = false;
    }

    public virtual void SetupNextSubLevel()
    {
        _subLevels[_currentSublevel].DisableTopBorder();
        
        _currentSublevel++;

        if (_currentSublevel >= _subLevels.Length)
        {
            TweenMainCameraToEnd();
            
            _canSpawnBubbles = false;
            
            return;
        }

        Initialize();

        ResetBubbleHits();
        RemoveBubblesListeners();
        Invoke(nameof(AddBubblesListeners), 1);
    }

    private void TweenMainCameraToEnd()
    {
        _mainCamera.transform.DOMoveY(_levelInfo.endLevelCameraY, CameraMovementDuration).onComplete +=
            MainCamera_EndAnimationComplete;
    }

    private void MainCamera_EndAnimationComplete()
    {
        OnLevelCompleted?.Invoke();
    }

    protected virtual void OnMouseDown()
    {
        if (!_canSpawnBubbles) 
            return;

        if (Camera.main != null)
            CreateBubble();
    }
    
    protected virtual void OnMouseDrag()
    {
        //this should be done in bubble but its collider is too small when created
        if (_canSpawnBubbles && _currentBubble)
            _currentBubble.HandleDrag();
    }

    protected virtual void OnMouseUp()
    {
        if (_canSpawnBubbles && _currentBubble)
            _currentBubble.HandleRelease();
    }

    protected virtual void CreateBubble()
    {
        var localMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        var subLevelColor = _levelInfo.subLevels[_currentSublevel].color;
        var pointsDeMultiplier = _levelInfo.subLevels[_currentSublevel].pointsDeMultiplier;
        
        _currentBubble = Main.Instance.Factory.GetBubble(localMousePosition, transform, subLevelColor, pointsDeMultiplier);
        _spawnedBubbles.Add(_currentBubble);

        AddAllBubbleListeners(_currentBubble);
        
        Main.Instance.ScoreUIHandler.DecreaseMoves();
        
        Main.Instance.Print("Create bubble...");
    }

    private void Bubble_OnDisabled(Bubble bubble)
    {
        RemoveAllBubbleListeners(bubble);
        
        _spawnedBubbles.Remove(bubble);

        if (_spawnedBubbles.Count == 0 && _currentSublevel >= _subLevels.Length)
            OnLevelCompleted?.Invoke();
    }

    private void Bubble_OnHitWhileDragged(Bubble bubble)
    {
        Main.Instance.Print("Destroy bubble...");
        Destroy(bubble.gameObject);
        
        RemoveAllBubbleListeners(bubble);
        
        _spawnedBubbles.Remove(bubble);

        _currentBubble = null;
    }

    private void Bubble_OnHit(Bubble bubble)
    {
        RemoveAllBubbleListeners(bubble);
        
        if (bubble.HitsCount < 2)
            Main.Instance.ScoreUIHandler.IncreaseCurrency(bubble.PointsReward / 10);

        if (_canSpawnBubbles)
            _subLevels[_currentSublevel].DecreasePoints(bubble.PointsReward);
    }

    private void ResetBubbleHits()
    {
        foreach (var item in _spawnedBubbles)
        {
            item.ResetHits();
        }
    }

    private void AddBubblesListeners()
    {
        foreach (var item in _spawnedBubbles)
        {
           AddAllBubbleListeners(item);
        }
    }
    
    private void RemoveBubblesListeners()
    {
        foreach (var item in _spawnedBubbles)
        {
            RemoveAllBubbleListeners(item);
        }
    }

    private void AddAllBubbleListeners(Bubble bubble)
    {
        bubble.OnHitBubble += Bubble_OnHit;
        bubble.OnHitTopBorder += Bubble_OnHit;
        bubble.OnHitWhileDragged +=Bubble_OnHitWhileDragged;
        bubble.OnDisabled += Bubble_OnDisabled;
    }

    private void RemoveAllBubbleListeners(Bubble bubble)
    {
        bubble.OnHitBubble -= Bubble_OnHit;
        bubble.OnHitTopBorder -= Bubble_OnHit;
        bubble.OnHitWhileDragged -=Bubble_OnHitWhileDragged;
        bubble.OnDisabled -= Bubble_OnDisabled;
    }
}
