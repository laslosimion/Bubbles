using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour, IRuntimeInitializable
{
    [SerializeField] private BubbleInfo _bubbleInfo;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _text;
    
    public event Action<Bubble> OnHitBubble;
    public event Action<Bubble> OnHitTopBorder;

    private int _pointsReward;

    private int _pointsDeMultiplier;
    private Color _defaultColor;

    public int PointsReward => int.Parse(_text.text);

    public void Initialize()
    {
        transform.localScale = Vector3.zero;
        
        _sprite.color = _defaultColor;
    }

    public void IncreaseScale()
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
        }
    }
}
