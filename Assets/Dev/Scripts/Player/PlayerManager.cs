using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PaintIn3D;
using Sirenix.OdinInspector;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class PlayerManager : MonoBehaviour
{
    private readonly Keyframe _lineStartKey = new Keyframe(0.007263184f, 0.05267715f);
    
    [SerializeField] private GameObject cleaner;
    [SerializeField] private LineRenderer cleanLine;
        
    private P3dHitBetween _hitBetween;
    private P3dPaintDecal _paintDecal;
    private IncrementalData _clearLevel;
    private GameManager _gameManager;
    private BoxCollider _boxCollider;
    
    public List<GameObject> weaponList;
    
    public Transform firstTr;
    public float defX;
    public float defZ;

    [FoldoutGroup("WaterJetModels"), SerializeField] private GameObject level1;
    [FoldoutGroup("WaterJetModels"), SerializeField] private GameObject level2;
    [FoldoutGroup("WaterJetModels"), SerializeField] private GameObject level3;

    private void Awake()
    {
        GetReferences();
        UpdateClearLevel();
    }

    private void GetReferences()
    {
        _hitBetween = cleaner.GetComponent<P3dHitBetween>();
        _paintDecal = cleaner.GetComponent<P3dPaintDecal>();
        _boxCollider = GetComponent<BoxCollider>();
        _clearLevel = Resources.Load<IncrementalData>("GlobalData/CleanLevelIncremental");
        _gameManager = GameManager.Instance;
    }
    

    private void Update()
    {
        CleanerPressureController();
    }

    private void CleanerPressureController()
    {
        if (_hitBetween.HitObj.TryGetComponent(out WeaponManager weaponManager))
        {
            if (weaponManager.weaponLevel <= _clearLevel.CurrentLevel * 10)
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
        WeaponPlateControl();
        _paintDecal.Scale = new Vector3(1 + _clearLevel.CurrentValue, _paintDecal.Scale.y,
            _paintDecal.Scale.z);
        
        var newCurve = new AnimationCurve();
        var lastKey = new Keyframe(1, 0.2f + _clearLevel.CurrentLevel * 0.09f);
        
        CheckWaterJetModel(newCurve);
        
        newCurve.AddKey(lastKey);

        cleanLine.widthCurve = newCurve;
    }

    private void CheckWaterJetModel(AnimationCurve newCurve)
    {
        switch (_clearLevel.CurrentLevel)
        {
            case < 3:
                JetModelChanger(1);
                newCurve.AddKey(_lineStartKey);
                break;
            case >= 3 and < 7:
                JetModelChanger(2);
                newCurve.AddKey(_lineStartKey);
                break;
            case >= 7 and < 11:
                JetModelChanger(3);
                newCurve.AddKey(new Keyframe(0.007263184f, 0.23f));
                break;
        }
    }

    private void JetModelChanger(int level)
    {
        level1.SetActive(level == 1);
        level2.SetActive(level == 2);
        level3.SetActive(level != 1 && level != 2);
    }

    private void WeaponPlateControl()
    {
        foreach (var plate in _gameManager.allPlates)
        {
            plate.CheckAvailable();
        }
    }
    
    public void SetPoses(Action onComplete = null)
    { 
        var lineCount = weaponList.Count / 3;
        var extraCount = weaponList.Count % 3;
        var localPosition = firstTr.localPosition;

        for (var i = 0; i < lineCount; i++)
        {
            StartCoroutine(SetObjectsPos(weaponList[(i * 3) + 0], localPosition + Vector3.back * (i * defZ), onComplete));
            StartCoroutine(SetObjectsPos(weaponList[(i * 3) + 1], localPosition + Vector3.back * (i * defZ) + Vector3.left * defX, onComplete));
            StartCoroutine(SetObjectsPos(weaponList[(i * 3) + 2], localPosition + Vector3.back * (i * defZ) + Vector3.right * defX, onComplete));
        }

        switch (extraCount)
        {
            case 1:
                StartCoroutine(SetObjectsPos(weaponList[(lineCount * 3) + 0], localPosition + Vector3.back * (lineCount * defZ), onComplete));
                break;
            case 2:
                StartCoroutine(SetObjectsPos(weaponList[(lineCount * 3) + 0], localPosition + Vector3.back * (lineCount * defZ) + Vector3.left * defX / 2, onComplete));
                StartCoroutine(SetObjectsPos(weaponList[(lineCount * 3) + 1], localPosition + Vector3.back * (lineCount * defZ) + Vector3.right * defX / 2, onComplete));
                break;
        }
    }
    
    private IEnumerator SetObjectsPos(GameObject go, Vector3 newPos, Action onComplete = null)
    {
        var tweenMove = go.transform.TweenLocalMove(newPos, Vector3.up * 180, 0.4f, () =>
        {
            go.transform.localScale = Vector3.one;
            onComplete?.Invoke();
        });
        return tweenMove;
    }

    internal void NarrowWeapons()
    {
        _boxCollider.center = new Vector3(0.0069f, 0.25f, -0.17f);
        _boxCollider.size = new Vector3(0.31f, 0.5f, 0.65f);
        defX -= 0.1f;
        SetPoses();
    }
}