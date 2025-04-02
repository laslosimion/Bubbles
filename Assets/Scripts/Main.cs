using UnityEngine;

public sealed class Main : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private PointsHandler _pointsHandler;
    [SerializeField] private UI _ui;

    [SerializeField] private bool _showPrints;
    
    public Factory Factory => _factory;
    public PointsHandler PointsHandler => _pointsHandler;
    
    private LevelBase _currentLevel;
    private int _currentLevelIndex;
    
    public static Main Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    private void Start()
    {
        Print("Show select level...");
        _ui.ShowLevelSelect();
    }

    public void CreateNextLevel()
    {
        Print("Create Next Level...");
        _currentLevelIndex++;
        
        CreateLevel();
    }
    
    public void CreateLevel(int index)
    {
        _currentLevelIndex = index;
        
        CreateLevel();
    }
    
    private void CreateLevel()
    {
        Print("Create Level #" + _currentLevelIndex);
        
        if (_currentLevel)
            Destroy(_currentLevel.gameObject);
        
        _pointsHandler.ResetValues();
        
        _currentLevel = _factory.GetLevel(_currentLevelIndex);
        _currentLevel.OnLevelCompleted += CurrentLevel_OnLevelCompleted;
        
        _ui.ShowLevelUI();
    }

    private void CurrentLevel_OnLevelCompleted()
    {
        Print("Level Completed...");
        
        _currentLevel.OnLevelCompleted -= CurrentLevel_OnLevelCompleted;
        
        WinLevel();
    }

    public void LoseSubLevel()
    {
        Print("Lose Sub Level...");
        if (_currentLevel)
        {
            Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }

        _ui.ShowLevelLost();
    }

    public void WinSubLevel()
    {
        Print("Win Sub Level...");
        _currentLevel.SetupNextSubLevel();
    }
    
    public void WinLevel()
    {
        Print("Win Level...");
        _ui.ShowLevelWon();
    }

    public void Print(string message)
    {
        if (_showPrints)
            Debug.Log(GetType() + $" {message}");
    }
}
