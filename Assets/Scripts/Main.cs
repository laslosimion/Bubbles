using UnityEngine;

public sealed class Main : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private PointsHandler _pointsHandler;
    public Factory Factory => _factory;
    public PointsHandler PointsHandler => _pointsHandler;
    
    private LevelBase _currentLevel;
    
    public static Main Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    private void Start()
    {
        CreateNextLevel();
    }

    private void CreateNextLevel()
    {
        _currentLevel = _factory.GetLevel(0);
        _currentLevel.OnLevelCompleted += CurrentLevel_OnLevelCompleted;
    }

    private void CurrentLevel_OnLevelCompleted()
    {
        WinLevel();
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
    }

    public void WinSubLevel()
    {
        _currentLevel.SetupNextSubLevel();
        Debug.Log("Win sublevel");
    }
    
    public void WinLevel()
    {
        Debug.Log("WinLevel");
    }
}
