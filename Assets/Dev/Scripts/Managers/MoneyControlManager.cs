using System;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneyControlManager : Singleton<MoneyControlManager>
{
    public Action OnMoneyChange;

    [SerializeField] private TextMeshProUGUI moneyText;
    public float currentMoney;

    [Header("Debug Mode")] public float money = 100;
    [SerializeField] private KeyCode keyCode = KeyCode.L;

    private void Awake()
    {
        currentMoney = PlayerPrefs.GetFloat("Money", 1);
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => UIManager.Instance);
        MoneyTextUpdate();
    }

    [Button]
    public bool ChangeMoney(float money)
    {
        if (!CheckCanBuy(money))
        {
            return false;
        }

        currentMoney += money;
        SetPlayerPrefs();
        MoneyTextUpdate();
        OnMoneyChange?.Invoke();
        return true;
    }

    public bool CheckCanBuy(float money)
    {
        return !(currentMoney + money < 0);
    }

    public void SetPlayerPrefs() => PlayerPrefs.SetFloat("Money", currentMoney);
    private void OnDisable() => SetPlayerPrefs();

    public void IncreaseMoney()
    {
        ChangeMoney(money);
    }

    public void DecreaseMoney()
    {
        ChangeMoney(-money);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            IncreaseMoney();
        }
    }


    private void MoneyTextUpdate()
    {
        moneyText.text = $"{FormatController.Format(currentMoney)}";
    }
}