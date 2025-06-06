using TMPro;
using UnityEngine;

public sealed class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _currencyText;
    [SerializeField] private AnimateText _animateText;
    
    private int _moves;
    private int _currency;

    private void Start()
    {
        UpdateCurrencyText(true);
    }

    public void ResetValues()
    {
        UpdateCurrencyText();
        UpdateMovesText();
    }

    public void IncreaseCurrency(int value, bool instant = false)
    {
        _currency += value;

        UpdateCurrencyText(instant);
    }

    public void DecreaseCurrency(int value, bool instant = false)
    {
        _currency--;

        UpdateCurrencyText(instant);
    }

    private void UpdateCurrencyText(bool instant = false)
    {
        if (instant || _animateText == null)
            _currencyText.text = _currency.ToString();
        else
            _animateText.Set(_currencyText, _currency);
    }
    
    public void DecreaseMoves()
    {
        _moves--;
        if (_moves <= 0)
        {
            Main.Instance.LoseSubLevel();
            
            if (_animateText)
                _animateText.Set(_movesText, 0);
            
            return;
        }

        UpdateMovesText();
    }

    public void IncreaseMoves(int value, bool instant = false)
    {
        _moves += value;

        UpdateMovesText(instant);
    }

    private void UpdateMovesText(bool instant = false)
    {
        if (!instant || _animateText == null)
            _animateText.Set(_movesText, _moves);
        else
            _movesText.text = _moves.ToString();
    }
}
