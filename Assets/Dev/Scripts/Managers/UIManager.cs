using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    
    [Header("GameScreens")] 
    [SerializeField] private GameObject tapToPlayScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject levelEndScreen;
    
    [Header("UIElements")]
    [SerializeField] private Button tapToPlayTrigger;

    private void Awake()
    {
        tapToPlayTrigger.onClick.AddListener(TapToPlayTrigger);
        _gameManager = GameManager.Instance;
    }

    private void TapToPlayTrigger()
    {
        tapToPlayScreen.SetActive(false);
        gameScreen.SetActive(true);
        _gameManager.gameStates = GameStates.Clean;
    }
}
