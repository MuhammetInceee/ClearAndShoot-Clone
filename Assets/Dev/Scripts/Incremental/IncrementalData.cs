using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "IncrementalData", menuName = "Data/Create Incremental Data")]
public class IncrementalData : ScriptableObjectPRO
{
    #region Scriptable Objects PRO

    public override void OnAwake()
    {
        _currentLevel = PlayerPrefs.GetInt(_prefsKey, 0);
        CheckAndSetMax();

        IsInitilazed = true;
        OnInitilazed?.Invoke();
    }

    public override void OnApplicationQuit()
    {
        IsInitilazed = false;
    }
    #endregion

    [SerializeField] private int _currentLevel;
    public int CurrentLevel => _currentLevel;


    [Header("Statuses Bools")] [SerializeField]
    private bool _isMax;

    public bool IsMax => _isMax;
    [HideInInspector] public bool IsInitilazed;


    #region Public Fields

    public float CurrentValue => GetValueFromLevel(_currentLevel);
    public float CurrentPrice => GetPriceFromLevel(_currentLevel);
    public float CurrentPriceInt => Mathf.FloorToInt(GetPriceFromLevel(_currentLevel));

    public int MaxLevelCount => (int)priceIncreaseCurve.keys[^1].time;
    #endregion

    #region Curves

    [Title("Current Value", "@CurrentValue", bold: false)] [Title("Current Money", "@CurrentPrice", bold: false)]
    
    [InfoBox("Don't forget to add startValue to last value", "@valueIncreaseCurve[0].value != 0")] 
    public AnimationCurve valueIncreaseCurve;
    [Title("")]
    [InfoBox("Don't forget to add startPrice to last value.", "@priceIncreaseCurve[0].value != 0")]
    public AnimationCurve priceIncreaseCurve;

    #endregion

    #region Actions

    public event Action OnLevelIncrease;
    private Action _onLevelMax;

    //Register to this action if you want to be notified when this variable changes value.
    public event Action OnLevelMax
    {
        add
        {
            if (IsInitilazed && _isMax)
            {
                value?.Invoke();
                return;
            }

            _onLevelMax += value;
        }
        remove { _onLevelMax -= value; }
    }

    public event Action OnInitilazed;

    #endregion

    #region Save

    private string _prefsKey => $"[{name}]{GetInstanceID()}";

    public void Save() => PlayerPrefs.SetInt(_prefsKey, _currentLevel);

    #endregion

    #region Private Methods

    private bool CheckAndSetMax()
    {
        _isMax = Mathf.Approximately(_currentLevel, MaxLevelCount);
        if (!_isMax) return false;
        _onLevelMax?.Invoke();

        return true;
    }

    #endregion

    #region Public Methods

    public void IncreaseLevel()
    {
        if (_isMax) return;

        _currentLevel++;
        CheckAndSetMax();

        Save();
        OnLevelIncrease?.Invoke();
    }

    public float GetValueFromLevel(int level)
    {
        return valueIncreaseCurve.Evaluate(level);
    }

    public float GetPriceFromLevel(int level)
    {
        return priceIncreaseCurve.Evaluate(level);
    }


    public int GetIntPriceFromLevel(int level)
    {
        return Mathf.FloorToInt(GetPriceFromLevel(level));
    }

    public void SetMax()
    {
        _currentLevel = MaxLevelCount;

        CheckAndSetMax();

        Save();
        OnLevelIncrease?.Invoke();
    }

    #endregion
}