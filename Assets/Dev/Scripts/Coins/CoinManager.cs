using PaintIn3D;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private const float CleanThreshold = 0.4f;
    
    private UIManager _uiManager;
    private Camera _mainCamera;
    private MoneyControlManager _moneyManager;
    private IncrementalData _incomeData;
    private P3dChannelCounter _counter;

    private bool _isReady;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _mainCamera = Camera.main;
        _moneyManager = MoneyControlManager.Instance;
        _incomeData = Resources.Load<IncrementalData>("GlobalData/IncomeIncremental");
        _counter = GetComponent<P3dChannelCounter>();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Clear()
    {
        if (_counter.RatioA < CleanThreshold || _isReady) return;
        _isReady = true;
        
        Collect();
    }

    private void Collect()
    {
        _uiManager.MoneyCollect(_mainCamera.WorldToScreenPoint(transform.position), () =>
            {
                _moneyManager.ChangeMoney(10 * _incomeData.CurrentValue);
                gameObject.SetActive(false);
            });
    }
}