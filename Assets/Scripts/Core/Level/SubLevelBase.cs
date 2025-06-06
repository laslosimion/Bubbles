using TMPro;
using UnityEngine;

public abstract class SubLevelBase : MonoBehaviour
{
    private const float EnableTopBorderDelay = 3;
    private const float TopPositionOffset = 3;
    
    [SerializeField] private Border _topBorder;
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private AnimateText _animateText;
    
    private int _points;

    public abstract void Initialize();
    
    public void DisableTopBorder()
    {
        _topBorder.gameObject.SetActive(false);
        Invoke(nameof(ResetTopBorder), EnableTopBorderDelay);
    }

    public void ResetTopBorder()
    {
        var topBorderTransform = _topBorder.transform;
        var topBorderTransformPosition = topBorderTransform.position;
        _topBorder.transform.position =
            new Vector3(topBorderTransformPosition.x, topBorderTransformPosition.y + TopPositionOffset);
        
        _topBorder.gameObject.SetActive(true);
    }

    public void IncreasePoints(int value, bool instant = false)
    {
        _points += value;

        UpdatePointsText(instant);
    }
    
    public void DecreasePoints(int value)
    {
        _points -= value;
        if (_points < 0)
            _points = 0;
        
        UpdatePointsText();
        
        if (_points <= 0)
            Main.Instance.WinSubLevel();
    }
    
    private void UpdatePointsText(bool instant = false)
    {
        if (instant || _animateText == null)
            _pointsText.text = _points.ToString();
        else
            _animateText.Set(_pointsText, _points);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(ResetTopBorder));
    }
}
