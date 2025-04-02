public class LevelWithObstacles : LevelBase
{
    public override void SetupNextSubLevel()
    {
        var subLevelWithObstacles = _subLevels[_currentSublevel] as SubLevelWithObstacles;
        subLevelWithObstacles.DestroyObstacles();
        
        base.SetupNextSubLevel();
    }
}
