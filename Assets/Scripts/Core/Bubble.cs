using System;
using TMPro;
using UnityEngine;

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
    private bool _hitTop;

    public bool IsDragged { get; private set; }

    public int PointsReward => int.Parse(_text.text);
    public int HitsCount { get; private set; }

    public void Initialize()
    {
        Main.Instance.Print("Initialize...");
        transform.localScale = Vector3.zero;
        
        _sprite.color = _defaultColor;
    }

    public void HandleDrag()
    {
        IncreaseScale();

        IsDragged = true;
        
        var localMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        transform.localPosition = new Vector3(localMousePosition.x, localMousePosition.y, 0);
    }
    
    public void HandleRelease()
    {
        IsDragged = false;
        Main.Instance.Print("Bubble released...");
    }
    
    public void ResetHits()
    {
        _hitTop = false;
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
        HitsCount++;

        var bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble && bubble._hitTop)
        {
            _hitTop = true;
            bubble._hitTop = true;
            
            OnHitBubble?.Invoke(this);
            return;
        }

        var border = other.gameObject.GetComponent<Border>();
        if (border && border.direction == Border.Direction.Top)
        {
            _hitTop = true;

            Main.Instance.Print("Bubble hit top. Points:" + PointsReward);
            OnHitTopBorder?.Invoke(this);
            return;
        }

        if (IsDragged && other.gameObject.GetComponent<Obstacle>())
            OnHitWhileDragged?.Invoke(this);
    }
}
