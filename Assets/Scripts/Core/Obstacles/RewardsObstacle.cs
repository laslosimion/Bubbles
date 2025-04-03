using TMPro;
using UnityEngine;


public class RewardsObstacle : MonoBehaviour
{
    [SerializeField] private int _pointsRequired = 100;
    [SerializeField] private TMP_Text _pointsTMPText;
    [SerializeField] private AnimateText _animateText;

    private void Start()
    {
        UpdateText(true);
    }

    private void UpdateText(bool instant = false)
    {
        if (instant || _animateText == null)
            _pointsTMPText.text = _pointsRequired.ToString();
        else
            _animateText.Set(_pointsTMPText, _pointsRequired);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bubble = other.gameObject.GetComponent<Bubble>();
        if (!bubble) 
            return;

        if (bubble.PointsReward > _pointsRequired)
            gameObject.SetActive(false);
        else
        {
            _pointsRequired -= bubble.PointsReward;
            UpdateText();
        }
        
        var bubblePointsAmount = bubble.PointsReward - _pointsRequired;

        if (bubblePointsAmount <= 0)
            bubble.gameObject.SetActive(false);
    }
}
