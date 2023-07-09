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
        print("aa");
        _animator.SetTrigger(Shoot1);
        var targetObj = GetAvailableBullet();
        
        targetObj.transform.localPosition = firePos.localPosition;
        targetObj.SetActive(true);
        targetObj.transform.SetParent(null);
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
        
        transform.SetParent(playerManager.transform);
        var tweenMove = transform.TweenLocalMove(Vector3.zero, Vector3.zero, 0.5f, () =>
        {
            _paintable.ClearAll(_texture.Texture, Color.white);
            playerManager.weaponList.Add(gameObject);
            _animator.enabled = true;
        });
        StartCoroutine(tweenMove);
    }
}
