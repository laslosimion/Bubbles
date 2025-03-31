using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IRuntimeInitializable
{
    [SerializeField] private LevelInfo _levelInfo;

    private bool _canSpawnBubbles;

    private List<Bubble> _spawnedBubbles = new();
    
    private Bubble _currentBubble;
    private int _currentSublevel;

    private void OnMouseDown()
    {
        if (!_canSpawnBubbles) 
            return;
        
        var localMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        _currentBubble =
            Factory.Instance.GetBubble(localMousePosition, transform, _levelInfo.subLevels[_currentSublevel].color);
        _spawnedBubbles.Add(_currentBubble);
    }

    private void OnMouseDrag()
    {
        if (_canSpawnBubbles && _currentBubble)
        {
            _currentBubble.IncreaseScale();
            //_currentBubble.transform.Translate(Input.mousePosition);
        }
    }

    public void Initialize()
    {
        _canSpawnBubbles = true;
    }
}
