using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelInfo:ScriptableObject
{
    [System.Serializable]
    public class SubLevel
    {
        public int moves;
        public int points;
        public float cameraSize = 25;
        public Color color;
    }
    
    public SubLevel[] subLevels;
}
