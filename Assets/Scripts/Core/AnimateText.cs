using System.Collections;
using TMPro;
using UnityEngine;

public class AnimateText : MonoBehaviour
{
    private Coroutine _animateCoroutine;
    
    public void Set(TMP_Text text, int value)
    {
        if (_animateCoroutine != null)
            StopCoroutine(_animateCoroutine);
        
        _animateCoroutine = StartCoroutine(IE_Animate(text, value));
    }

    private static IEnumerator IE_Animate(TMP_Text text, int value)
    {
        var initialValue = int.Parse(text.text);

        if (initialValue > value)
        {
            var valueToModifyBy = initialValue - value;

            while (valueToModifyBy > 0)
            {
                text.text = (int.Parse(text.text) - 1).ToString();
                valueToModifyBy--;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            var valueToModifyBy = value - initialValue;
            
            while (valueToModifyBy > 0)
            {
                text.text = (int.Parse(text.text) + 1).ToString();
                valueToModifyBy--;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
