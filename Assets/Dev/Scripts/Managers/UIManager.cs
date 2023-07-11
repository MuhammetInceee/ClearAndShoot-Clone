using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    
    [Header("GameScreens")] 
    [SerializeField] private GameObject tapToPlayScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject levelEndScreen;
    [SerializeField] private GameObject alwaysActiveScreen;
    
    [Header("UIElements")]
    [SerializeField] private Button tapToPlayTrigger;
    [SerializeField] private Button levelEndTrigger;
    [SerializeField] private Transform moneyImage;
    [SerializeField] private GameObject moneySpritePrefab;
    [SerializeField] private TextMeshProUGUI levelText;


    private void Awake()
    {
        tapToPlayTrigger.onClick.AddListener(TapToPlayTrigger);
        levelEndTrigger.onClick.AddListener(LevelManager.Instance.NextLevel);
        _gameManager = GameManager.Instance;
        levelText.text = $"Lv. {LevelManager.Instance.GetCurrentLevel()}";
    }

    private void TapToPlayTrigger()
    {
        tapToPlayScreen.SetActive(false);
        gameScreen.SetActive(true);
        _gameManager.gameStates = GameStates.Clean;
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
