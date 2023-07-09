using System;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneyControlManager : Singleton<MoneyControlManager>
{
    public Action OnMoneyChange;

    [SerializeField] private TextMeshProUGUI[] moneyTexts;
    public float currentMoney;

    [Header("Debug Mode")] public float money = 100;
    public KeyCode keyCode = KeyCode.L;

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
        else
        {
            currentMoney += money;
            SetPlayerPrefs();
            MoneyTextUpdate();
            OnMoneyChange?.Invoke();
            return true;
        }
    }

    public bool CheckCanBuy(float money)
    {
        if (currentMoney + money < 0)
        {
            return false;
        }
        else
            return true;
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
        foreach (var text in moneyTexts)
        {
            text.text = $"{FormatController.Format(currentMoney)}";
            text.transform.localScale = Vector3.one;
            // text.transform.DOKill();
            // text.transform.DOPunchScale(Vector3.one * 0.25f, .25f, 1, 0.1f).SetEase(Ease.InOutBounce);
        }
    }
}