using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelBase : MonoBehaviour, IRuntimeInitializable
{
    public event Action OnLevelCompleted;
    
    [SerializeField] protected LevelInfo _levelInfo;
    [SerializeField] protected SubLevelBase[] _subLevels;

    protected int _currentSublevel;
    
    private bool _canSpawnBubbles;
    private readonly List<Bubble> _spawnedBubbles = new();
    private Bubble _currentBubble;
    
    private Camera _mainCamera;

    public virtual void Initialize()
    {
        _canSpawnBubbles = true;

        Main.Instance.PointsHandler.IncreaseMoves(_levelInfo.subLevels[_currentSublevel].moves);
        Main.Instance.PointsHandler.IncreasePoints(_levelInfo.subLevels[_currentSublevel].points);

        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError(GetType() + "Can not find main camera!");
            return;
        }

        var mainCameraPosition = _mainCamera.transform.position;
        _mainCamera.orthographicSize = _levelInfo.subLevels[_currentSublevel].cameraSize;
        _mainCamera.transform.position = new Vector3(mainCameraPosition.x, _levelInfo.subLevels[_currentSublevel].cameraY, mainCameraPosition.z);
        
        _subLevels[_currentSublevel].Initialize();
    }

    public virtual void SetupNextSubLevel()
    {
        _subLevels[_currentSublevel].DisableTopBorder();
        
        _currentSublevel++;

        if (_currentSublevel >= _subLevels.Length)
        {
            OnLevelCompleted?.Invoke();
            _canSpawnBubbles = false;
            
            return;
        }

        Initialize();

        ResetBubbleHits();
        RemoveBubblesListeners();
        Invoke(nameof(AddBubblesListeners), 1);
    }
    
    protected virtual void OnMouseDown()
    {
        if (!_canSpawnBubbles) 
            return;

        if (Camera.main != null)
            CreateBubble();

        Main.Instance.PointsHandler.DecreaseMoves();
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

        _currentBubble.OnHitBubble += Bubble_OnHit;
        _currentBubble.OnHitTopBorder += Bubble_OnHit;
        _currentBubble.OnHitWhileDragged +=Bubble_OnHitWhileDragged;
        
        Main.Instance.PointsHandler.DecreaseMoves();
    }

    private void Bubble_OnHitWhileDragged(Bubble bubble)
    {
        Destroy(bubble.gameObject);
        
        bubble.OnHitBubble -= Bubble_OnHit;
        bubble.OnHitTopBorder -= Bubble_OnHit;
        bubble.OnHitWhileDragged -=Bubble_OnHitWhileDragged;
        
        _spawnedBubbles.Remove(bubble);

        _currentBubble = null;
    }

    private void Bubble_OnHit(Bubble bubble)
    {
        bubble.OnHitBubble -= Bubble_OnHit;
        bubble.OnHitTopBorder -=Bubble_OnHit;
        bubble.OnHitWhileDragged -=Bubble_OnHitWhileDragged;
        
        if (bubble.HitsCount < 2)
            Main.Instance.PointsHandler.IncreaseCurrency(bubble.PointsReward / 10);
        
        Main.Instance.PointsHandler.DecreasePoints(bubble.PointsReward);
    }

    private void ResetBubbleHits()
    {
        foreach (var item in _spawnedBubbles)
        {
            item.ResetHits();
        }
    }

    protected virtual void OnDestroy()
    {
        RemoveBubblesListeners();

        _spawnedBubbles.Clear();

        _canSpawnBubbles = false;
    }

    private void AddBubblesListeners()
    {
        foreach (var item in _spawnedBubbles)
        {
            item.OnHitBubble += Bubble_OnHit;
            item.OnHitTopBorder += Bubble_OnHit;
            item.OnHitWhileDragged +=Bubble_OnHitWhileDragged;
        }
    }
    
    private void RemoveBubblesListeners()
    {
        foreach (var item in _spawnedBubbles)
        {
            item.OnHitBubble -= Bubble_OnHit;
            item.OnHitTopBorder -= Bubble_OnHit;
            item.OnHitWhileDragged -=Bubble_OnHitWhileDragged;
        }
    }
}
