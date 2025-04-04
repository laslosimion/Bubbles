using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public sealed class LevelInfo:ScriptableObject
{
    [System.Serializable]
    public class SubLevel
    {
        public int moves;
        
        public int points;
        public int pointsDeMultiplier = 30;
        
        public float cameraSize = 25;
        public float cameraY;
        
        public Color color;
    }
    
    public SubLevel[] subLevels;
    public float endLevelCameraY = 55;
}
