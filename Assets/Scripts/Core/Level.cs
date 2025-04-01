using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IRuntimeInitializable
{
    [SerializeField] private LevelInfo _levelInfo;

    private bool _canSpawnBubbles;

    private readonly List<Bubble> _spawnedBubbles = new();
    
    private Bubble _currentBubble;
    private int _currentSublevel;

    private void OnMouseDown()
    {
        if (!_canSpawnBubbles) 
            return;

        if (Camera.main != null)
        {
            var localMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            var subLevelColor = _levelInfo.subLevels[_currentSublevel].color;
            CreateBubble(localMousePosition, subLevelColor);
        }

        Main.Instance.PointsHandler.DecreaseMoves();
    }
    
    private void OnMouseDrag()
    {
        if (_canSpawnBubbles && _currentBubble)
        {
            _currentBubble.IncreaseScale();
            //_currentBubble.transform.Translate(Input.mousePosition);
        }
    }

    private void CreateBubble(Vector3 localMousePosition, Color subLevelColor)
    {
        _currentBubble = Main.Instance.Factory.GetBubble(localMousePosition, transform, subLevelColor);
        _spawnedBubbles.Add(_currentBubble);

        _currentBubble.OnHitBubble += Bubble_OnHit;
        _currentBubble.OnHitTopBorder += Bubble_OnHit;
        
        Main.Instance.PointsHandler.DecreaseMoves();
    }

    private static void Bubble_OnHit(Bubble bubble)
    {
        bubble.OnHitBubble -= Bubble_OnHit;
        bubble.OnHitTopBorder -=Bubble_OnHit;
        
        Main.Instance.PointsHandler.DecreasePoints(bubble.PointsReward);
    }

    public void Initialize()
    {
        _canSpawnBubbles = true;

        Main.Instance.PointsHandler.IncreaseMoves(_levelInfo.subLevels[_currentSublevel].moves);
        Main.Instance.PointsHandler.IncreasePoints(_levelInfo.subLevels[_currentSublevel].points);
        
        if (Camera.main != null) 
            Camera.main.orthographicSize = _levelInfo.subLevels[_currentSublevel].cameraSize;
    }

    private void OnDestroy()
    {
        foreach (var item in _spawnedBubbles)
        {
            item.OnHitBubble -= Bubble_OnHit;
            item.OnHitTopBorder -=Bubble_OnHit;
        }
        
        _spawnedBubbles.Clear();
    }
}
