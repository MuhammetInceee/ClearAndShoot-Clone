using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PaintIn3D;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject cleaner;
    [SerializeField] private LineRenderer cleanLine;
        
    private Keyframe _lineStartKey = new Keyframe(0.007263184f, 0.05267715f);
    private P3dHitBetween _hitBetween;
    private P3dPaintDecal _paintDecal;
    private IncrementalData _clearLevel;

    // public int currentCleanLevel;
    public List<GameObject> weaponList;
    
    public Transform firstTr;
    public float defX;
    public float defZ;


    private void Awake()
    {
        GetReferences();
        UpdateClearLevel();
    }

    private void GetReferences()
    {
        _hitBetween = cleaner.GetComponent<P3dHitBetween>();
        _paintDecal = cleaner.GetComponent<P3dPaintDecal>();
        _clearLevel = Resources.Load<IncrementalData>("GlobalData/CleanLevelIncremental");
    }
    

    private void Update()
    {
        CleanerPressureController();
    }

    private void CleanerPressureController()
    {
        if (_hitBetween.HitObj.TryGetComponent(out WeaponManager weaponManager))
        {
            if (weaponManager.weaponLevel <= _clearLevel.CurrentLevel)
            {
                _paintDecal.Opacity = 1;
                weaponManager.Clear(this);
            }
            else _paintDecal.Opacity = 0.01f;
        }
        else if (_hitBetween.HitObj.TryGetComponent(out CoinManager coinManager))
        {
            _paintDecal.Opacity = 1;
            coinManager.Clear();
        }
        else _paintDecal.Opacity = 1;
    }

    public void UpdateClearLevel()
    {
        _paintDecal.Scale = new Vector3(1 + _clearLevel.CurrentValue, _paintDecal.Scale.y,
            _paintDecal.Scale.z);
        
        var newCurve = new AnimationCurve();
        var lastKey = new Keyframe(1, 0.2f + _clearLevel.CurrentLevel * 0.09f);
        
        newCurve.AddKey(_lineStartKey);
        newCurve.AddKey(lastKey);

        cleanLine.widthCurve = newCurve;
    }
}