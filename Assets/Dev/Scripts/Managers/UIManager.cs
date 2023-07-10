using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    private Camera _mainCamera;
    
    [Header("GameScreens")] 
    [SerializeField] private GameObject tapToPlayScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject levelEndScreen;
    [SerializeField] private GameObject alwaysActiveScreen;
    
    [Header("UIElements")]
    [SerializeField] private Button tapToPlayTrigger;
    [SerializeField] private Transform moneyImage;
    [SerializeField] private GameObject moneySpritePrefab;


    private void Awake()
    {
        tapToPlayTrigger.onClick.AddListener(TapToPlayTrigger);
        _gameManager = GameManager.Instance;
        _mainCamera = Camera.main;
    }

    private void TapToPlayTrigger()
    {
        tapToPlayScreen.SetActive(false);
        gameScreen.SetActive(true);
        _gameManager.gameStates = GameStates.Clean;
    }
    
    public void RestartGameViaSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OpenFinishRect()
    {
        gameScreen.SetActive(false);
        levelEndScreen.SetActive(true);
    }

    public void MoneyCollect(Vector3 startPos, Action onComplete)
    {
        var spawn = Instantiate(moneySpritePrefab, alwaysActiveScreen.transform, true);
        
        spawn.transform.position = startPos;

        var tweenMove = spawn.transform.TweenMove(moneyImage.position, 1f, () =>
        {
            Destroy(spawn);
            onComplete.Invoke();
        });
        StartCoroutine(tweenMove);
    }
}
