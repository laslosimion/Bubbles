using UnityEngine;

public class CurrencyRewardsObstacle : MonoBehaviour
{
    [SerializeField] private int _currencyRewardAmount = 20;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Bubble>()) 
            return;
        
        Main.Instance.PointsHandler.IncreaseCurrency(_currencyRewardAmount);
        gameObject.SetActive(false);
    }
}
