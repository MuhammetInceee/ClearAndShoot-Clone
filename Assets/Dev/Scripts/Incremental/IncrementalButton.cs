using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IncrementalButton : MonoBehaviour
{
    public IncrementalData incrementalData;

    [Header("References")] [SerializeField]
    References _references;

    [Header("Events")] public UnityEventsStruct UnityEvents;

    [Header("Prefix")] [SerializeField] string _pricePrefix = "${value}";
    [SerializeField] string _levelPrefix = "Level {value}";

    private int _level;

    private void OnEnable()
    {
        incrementalData.OnInitilazed += UpdateValues;

        MoneyControlManager.Instance.OnMoneyChange += UpdateValues;
        incrementalData.OnLevelMax += SetMaxTexts;

        UpdateValues(); // First Initilaze
    }

    private void OnDisable()
    {
        if (MoneyControlManager.Instance)
            MoneyControlManager.Instance.OnMoneyChange -= UpdateValues;


        incrementalData.OnLevelMax -= SetMaxTexts;
        incrementalData.OnInitilazed -= UpdateValues;
    }

    private void UpdateValues()
    {
        SetTexts();
        if (MoneyControlManager.Instance.CheckCanBuy(-incrementalData.CurrentPriceInt))
        {
            if (!incrementalData.IsMax)
            {
                UnityEvents.OnCanBuy?.Invoke();
            }
        }
        else
        {
            UnityEvents.OnNotEnoughMoney?.Invoke();
        }
    }

    public void Buy()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.F))
        {
            incrementalData.SetMax();
            return;
        }
#endif
        if (!MoneyControlManager.Instance.CheckCanBuy(-incrementalData.CurrentPriceInt) ||
            incrementalData.IsMax) return;

        MoneyControlManager.Instance.ChangeMoney(-incrementalData.CurrentPriceInt);
        incrementalData.IncreaseLevel();
        UnityEvents.OnBought?.Invoke();
        UpdateValues();
    }

    private void SetMaxTexts()
    {
        _references.levelText.text = "Max";
        Debug.Log($"<color=orange><b>Max</b></color>");
        UnityEvents.OnMax?.Invoke();
    }

    private void SetTexts()
    {
        if (incrementalData.IsMax)
        {
            return;
        }

        string text = string.Empty;

        text = _levelPrefix.Replace("{value}", $"{incrementalData.CurrentLevel + 1}");
        _references.levelText.text = text;

        text = _pricePrefix.Replace("{value}", $"{incrementalData.CurrentPriceInt}");
        _references.priceText.text = text;
    }

    [Serializable]
    public struct UnityEventsStruct
    {
        public UnityEvent OnBought;
        public UnityEvent OnCanBuy;
        public UnityEvent OnNotEnoughMoney;
        public UnityEvent OnMax;
    }

    [Serializable]
    private struct References
    {
        [Header("Base Button")] [SerializeField]
        public Button button;

        [SerializeField] public TMP_Text levelText;
        [SerializeField] public TMP_Text priceText;
    }
}