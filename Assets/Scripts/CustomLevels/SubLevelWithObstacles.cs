using UnityEngine;

public class SubLevelWithObstacles : SubLevelBase
{
    [SerializeField] private Obstacle[] _obstacles;
    
    public override void Initialize()
    {
        foreach (var item in _obstacles)
        {
            item.Initialize();
        }
    }
    
    public void DestroyObstacles()
    {
        foreach (var item in _obstacles)
        {
            Destroy(item.gameObject);
        }
    }
}
