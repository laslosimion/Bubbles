using UnityEngine;

public abstract class SubLevelBase : MonoBehaviour
{
    private const float EnableTopBorderDelay = 3;
    private const float TopPositionOffset = 3;
    
    [SerializeField] private Border _topBorder;

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

    private void OnDestroy()
    {
        CancelInvoke(nameof(ResetTopBorder));
    }
}
