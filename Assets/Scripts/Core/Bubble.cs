using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class Bubble : MonoBehaviour, IRuntimeInitializable
{
    [SerializeField] private BubbleInfo _bubbleInfo;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _text;
    
    public event Action<Bubble> OnHitBubble;
    public event Action<Bubble> OnHitTopBorder;
    public event Action<Bubble> OnHitWhileDragged;

    private int _pointsReward;

    private int _pointsDeMultiplier;
    private Color _defaultColor;
    private bool _isDragged;

    public bool IsDragged => _isDragged;
    public int PointsReward => int.Parse(_text.text);

    public void Initialize()
    {
        transform.localScale = Vector3.zero;
        
        _sprite.color = _defaultColor;
    }

    public void HandleDrag()
    {
        IncreaseScale();

        _isDragged = true;
        
        var localMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        transform.localPosition = new Vector3(localMousePosition.x, localMousePosition.y, 0);
    }
    
    public void HandleRelease()
    {
        _isDragged = false;
    }

    private void IncreaseScale()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(localScale.x + _bubbleInfo.increaseScaleSpeed,
            localScale.y + _bubbleInfo.increaseScaleSpeed);

        _pointsReward += _bubbleInfo.increasePointsSpeed;
        _text.text = (_pointsReward / _pointsDeMultiplier).ToString();
    }

    public void SetPointsDemultiplier(int value)
    {
        _pointsDeMultiplier = value;
    }
    
    public void SetDefaultColor(Color color)
    {
        _defaultColor = color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bubble>())
        {
            OnHitBubble?.Invoke(this);
            return;
        }

        var border = other.gameObject.GetComponent<Border>();
        if (border && border.direction == Border.Direction.Top)
        {
            OnHitTopBorder?.Invoke(this);
            return;
        }

        if (_isDragged && other.gameObject.GetComponent<Obstacle>())
        {
            OnHitWhileDragged?.Invoke(this);
        }
    }
}
