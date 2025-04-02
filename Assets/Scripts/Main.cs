using UnityEngine;

public sealed class Main : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private PointsHandler _pointsHandler;
    [SerializeField] private UI _ui;
    
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
        _ui.ShowLevelSelect();
    }

    public void CreateNextLevel()
    {
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
        if (_currentLevel)
            Destroy(_currentLevel.gameObject);
        
        _pointsHandler.ResetValues();
        
        _currentLevel = _factory.GetLevel(_currentLevelIndex);
        _currentLevel.OnLevelCompleted += CurrentLevel_OnLevelCompleted;
        
        _ui.ShowLevelUI();
    }

    private void CurrentLevel_OnLevelCompleted()
    {
        _currentLevel.OnLevelCompleted -= CurrentLevel_OnLevelCompleted;
        
        WinLevel();
    }

    public void EndGame()
    {
        if (_currentLevel)
        {
            Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }

        _ui.ShowLevelLost();
    }

    public void WinSubLevel()
    {
        _currentLevel.SetupNextSubLevel();
        Debug.Log("Win sublevel");
    }
    
    public void WinLevel()
    {
        _ui.ShowLevelWon();
    }
}
