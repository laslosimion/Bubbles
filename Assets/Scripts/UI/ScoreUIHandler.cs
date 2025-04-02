using TMPro;
using UnityEngine;

public sealed class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _currencyText;

    
    private int _moves;
    private int _currency;

    private void Start()
    {
        UpdateCurrencyText();
    }

    public void ResetValues()
    {
        UpdateCurrencyText();
        UpdateMovesText();
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
    
    public void DecreaseMoves()
    {
        _moves--;
        if (_moves <= 0)
        {
            Main.Instance.LoseSubLevel();
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
