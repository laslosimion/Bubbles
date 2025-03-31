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

    public int PointsReward { get; private set; } = 0;

    public void Initialize()
    {
        transform.localScale = Vector3.zero;
    }

    public void IncreaseScale()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(localScale.x + _bubbleInfo.increaseScaleSpeed,
            localScale.y + _bubbleInfo.increaseScaleSpeed);

        PointsReward += _bubbleInfo.increasePointsSpeed;
        _text.text = (PointsReward / _bubbleInfo.pointsDeMultiplier).ToString();
    }

    public void SetColor(Color color)
    {
        _sprite.color = color;
    }
}
