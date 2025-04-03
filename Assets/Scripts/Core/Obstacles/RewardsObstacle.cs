using TMPro;
using UnityEngine;

public class RewardsObstacle : MonoBehaviour
{
    [SerializeField] private int _pointsRequired = 100;
    [SerializeField] private TMP_Text _pointsTMPText;

    private void Start()
    {
        UpdateText();
    }
    
    private void UpdateText()
    {
        _pointsTMPText.text = _pointsRequired.ToString();
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
