using TMPro;
using UnityEngine;

public sealed class PointsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _currencyText;

    private int _points;
    private int _moves;
    private int _currency;

    private void Start()
    {
        UpdateCurrencyText();
        
        UpdatePointsText();
    }

    public void ResetValues()
    {
        _points = _moves = _currency = 0;
        
        UpdateCurrencyText();
        UpdateMovesText();
        UpdatePointsText();
    }

    public void IncreaseCurrency(int value)
    {
        _currency += value;

        UpdateCurrencyText();
    }

    public void DecreaseCurrency(int value)
    {
        _currency--;

        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        _currencyText.text = _currency.ToString();
    }

    public void IncreasePoints(int value)
    {
        _points += value;

        UpdatePointsText();
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

    private void UpdatePointsText()
    {
        _pointsText.text = _points.ToString();
    }
    
    public void DecreaseMoves()
    {
        _moves--;
        if (_moves <= 0)
        {
            Main.Instance.EndGame();
            _movesText.text = "0";
            return;
        }

        UpdateMovesText();
    }
    
    public void IncreaseMoves(int value)
    {
        _moves += value;

        UpdateMovesText();
    }

    private void UpdateMovesText()
    {
        _movesText.text = _moves.ToString();
    }
}
