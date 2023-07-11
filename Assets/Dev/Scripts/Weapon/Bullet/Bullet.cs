using UnityEngine;
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

public class Bullet : MonoBehaviour
{
    private const float BulletLifeTime = 2;
    
    internal float damage;
    
    private float _startTime;
    private Transform _parent;
    private BulletType _bulletType;
    
    [SerializeField] private BulletTypes bulletTypes;
    
    private void OnEnable()
    {
        _bulletType = new BulletType(bulletTypes);
        if (_parent == null) _parent = transform.parent;
        _startTime = Time.time;
    }
    private void Update()
    {
        transform.Translate(_bulletType.direction);
        if (Time.time >= _startTime + BulletLifeTime) GetBackPool();
    }
    internal void GetBackPool()
    {
        var transform1 = transform;
        
        transform1.position = Vector3.zero;
        transform1.SetParent(_parent);
        gameObject.SetActive(false);
        damage = 0;
    }
}

public enum BulletTypes{gun, hammer, knife, grenade}

public class BulletType
{
    internal static readonly BulletType gun = new(BulletTypes.gun);
    internal static readonly BulletType hammer = new(BulletTypes.hammer);
    internal static readonly BulletType knife = new(BulletTypes.knife);
    internal static readonly BulletType grenade = new(BulletTypes.grenade);

    private readonly BulletTypes bulletTypes;

    internal BulletType(BulletTypes bulletTypes)
    {
        this.bulletTypes = bulletTypes;
    }

    internal Vector3 direction => bulletTypes switch
    {
        BulletTypes.gun => Vector3.forward * (5 * Time.deltaTime),
        BulletTypes.hammer => Vector3.left * (5 * Time.deltaTime),
        BulletTypes.knife => Vector3.left * (5 * Time.deltaTime),
        BulletTypes.grenade => Vector3.down * (5 * Time.deltaTime),
    };
}
