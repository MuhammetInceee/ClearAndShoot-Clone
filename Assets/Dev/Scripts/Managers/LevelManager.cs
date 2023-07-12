using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private int _currentLevel;

    [SerializeField] private List<GameObject> levels;

    private void Awake()
    {
        InstantiateLevel();
    }

    private void InstantiateLevel()
    {
        Instantiate(levels[GetCurrentLevel() % 3]);
    }
    
    internal void NextLevel()
    {
        _currentLevel = GetCurrentLevel();
        _currentLevel++;
        PlayerPrefs.SetInt("Level", _currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    internal int GetCurrentLevel()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 1);
        return _currentLevel;
    }
}
