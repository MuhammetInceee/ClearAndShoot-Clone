using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PaintIn3D;
using Sirenix.OdinInspector;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class WeaponManager : MonoBehaviour
{
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");
    private const float CleanThreshold = 0.4f;

    private P3dChannelCounter _counter;
    private P3dPaintable _paintable;
    private P3dPaintableTexture _texture;
    private GameManager _gameManager;
    private Animator _animator;
    private IncrementalData _globalFireRate;
    private IncrementalData _globalDamage;
    private bool _isReady;
    private float _lastShootTime = 0f;

    [FoldoutGroup("PoolFeatures"), SerializeField] private GameObject bulletPrefab;
    [FoldoutGroup("PoolFeatures"), SerializeField] private float poolSize;
    [FoldoutGroup("PoolFeatures"), SerializeField]private Transform poolParent;
    [FoldoutGroup("PoolFeatures"), SerializeField]private List<GameObject> bulletPool;

    
    [FoldoutGroup("WeaponFeatures") ,SerializeField] private Transform firePos;
    [FoldoutGroup("WeaponFeatures") ,SerializeField] private float fireRate;
    [FoldoutGroup("WeaponFeatures") ,SerializeField] private float damage;
    [FoldoutGroup("WeaponFeatures")] public int weaponLevel;
    

    private void Awake()
    {
        GetReferences();
        InitVariables();
        PoolInit();
    }

    private void GetReferences()
    {
        _paintable = GetComponent<P3dPaintable>();
        _counter = GetComponent<P3dChannelCounter>();
        _texture = GetComponent<P3dPaintableTexture>();
        _animator = GetComponent<Animator>();
        _gameManager = GameManager.Instance;
    }

    private void InitVariables()
    {
        _globalFireRate = Resources.Load<IncrementalData>("GlobalData/FireRateIncremental");
        _globalDamage = Resources.Load<IncrementalData>("GlobalData/DamageIncremental");
    }

    private void PoolInit()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, poolParent);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(!_isReady || _gameManager.gameStates != GameStates.Shoot) return;
        if(!(Time.time > _lastShootTime + fireRate)) return;
        
        Fire();
        _lastShootTime = Time.time;
    }

    private void Fire()
    {
        _animator.SetTrigger(Shoot1);
        var targetObj = GetAvailableBullet();
        
        targetObj.transform.SetParent(null);
        targetObj.transform.position = firePos.position;
        targetObj.SetActive(true);
    }

    private GameObject GetAvailableBullet()
    {
        var firstInactiveObject = bulletPool.FirstOrDefault(go => !go.activeSelf);
        return firstInactiveObject;
    }

    public void Clear(PlayerManager playerManager)
    {
        if (_counter.RatioA < CleanThreshold || _isReady) return;

        _isReady = true;
        
        // transform.SetParent(playerManager.transform);
        // var tweenMove = transform.TweenLocalMove(Vector3.zero, Vector3.zero, 0.5f, () =>
        // {
        //     _paintable.ClearAll(_texture.Texture, Color.white);
        //     playerManager.weaponList.Add(gameObject);
        //     _animator.enabled = true;
        // });
        // StartCoroutine(tweenMove);
        
        playerManager.weaponList.Add(gameObject);
        transform.SetParent(playerManager.transform);
        
        SetPoses(playerManager.weaponList, playerManager.firstTr, playerManager.defX, playerManager.defZ);
    }
    
    private void SetPoses(List<GameObject> objects, Transform firstTr, float defX, float defZ)
    { 
        var lineCount = objects.Count / 3;
        var extraCount = objects.Count % 3;
        var localPosition = firstTr.localPosition;

        for (var i = 0; i < lineCount; i++)
        {
            StartCoroutine(SetObjectsPos(objects[(i * 3) + 0], localPosition + Vector3.back * (i * defZ)));
            StartCoroutine(SetObjectsPos(objects[(i * 3) + 1], localPosition + Vector3.back * (i * defZ) + Vector3.left * defX));
            StartCoroutine(SetObjectsPos(objects[(i * 3) + 2], localPosition + Vector3.back * (i * defZ) + Vector3.right * defX));
        }

        switch (extraCount)
        {
            case 1:
                StartCoroutine(SetObjectsPos(objects[(lineCount * 3) + 0], localPosition + Vector3.back * (lineCount * defZ)));
                break;
            case 2:
                StartCoroutine(SetObjectsPos(objects[(lineCount * 3) + 0], localPosition + Vector3.back * (lineCount * defZ) + Vector3.left * defX / 2));
                StartCoroutine(SetObjectsPos(objects[(lineCount * 3) + 1], localPosition + Vector3.back * (lineCount * defZ) + Vector3.right * defX / 2));
                break;
        }

        
    }

    private IEnumerator SetObjectsPos(GameObject go, Vector3 newPos)
    {
        // while (Vector3.Distance(go.transform.localPosition, newPos) > 0.01f)
        // {
        //     go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition, newPos, Time.deltaTime * 20);
        //     yield return null;
        // }
        var tweenMove = go.transform.TweenLocalMove(newPos, Vector3.up * 180, 0.1f, () =>
        {
            _paintable.ClearAll(_texture.Texture, Color.white);
            _animator.enabled = true;
        });
        return tweenMove;
    }
}
