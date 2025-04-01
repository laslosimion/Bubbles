using UnityEngine;

public class SubLevel : MonoBehaviour
{
    private const float EnableTopBorderDelay = 3;
    private const float TopPositionOffset = 3;
    
    [SerializeField] private Obstacle[] _obstacles;
    [SerializeField] private Border _topBorder;
    
    public void Initialize()
    {
        foreach (var item in _obstacles)
        {
            item.Initialize();
        }
    }

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

    public void DestroyObstacles()
    {
        foreach (var item in _obstacles)
        {
            Destroy(item.gameObject);
        }
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(ResetTopBorder));
    }
}
