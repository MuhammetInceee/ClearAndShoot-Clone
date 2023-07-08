using PaintIn3D;
using UnityEngine;

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

public class WeaponManager_OldVersion : MonoBehaviour
{
    private const float CleanThreshold = 0.4f;

    private P3dChannelCounter _counter;
    private bool _isReady;
    
    [SerializeField] private WeaponFeatures weaponFeatures;
    [SerializeField] private WeaponTypes weaponTypes;

    internal WeaponType type => new(weaponTypes, weaponFeatures);

    public int weaponLevel;

    private void Awake() => _counter = GetComponent<P3dChannelCounter>();

    public void Clear(Transform player)
    {
        if (_counter.RatioA < CleanThreshold || _isReady) return;

        _isReady = true;
        
        //TODO Object Collect Mechanic 
        transform.SetParent(player);
        var tweenMove = transform.TweenLocalMove(Vector3.zero, Vector3.zero, 1f, () =>
        {
            this.enabled = false;
        });
        StartCoroutine(tweenMove);


    }
}

public enum WeaponTypes {grenade,hammer,knife,p90,pistol }

public class WeaponType
{
    internal static WeaponType granade = new(WeaponTypes.grenade, new WeaponFeatures());
    internal static WeaponType hammer = new(WeaponTypes.hammer, new WeaponFeatures());
    internal static WeaponType knife = new(WeaponTypes.knife, new WeaponFeatures());
    internal static WeaponType p90 = new(WeaponTypes.p90, new WeaponFeatures());
    internal static WeaponType pistol = new(WeaponTypes.pistol, new WeaponFeatures());

    private readonly WeaponTypes weaponTypes;
    private readonly WeaponFeatures weaponFeatures;

    internal WeaponType(WeaponTypes weaponTypes, WeaponFeatures weaponFeatures)
    {
        this.weaponTypes = weaponTypes;
        this.weaponFeatures = weaponFeatures;
    }

    internal float fireRate => weaponTypes switch
    {
        WeaponTypes.grenade => weaponFeatures.fireRate,
        WeaponTypes.hammer => weaponFeatures.fireRate,
        WeaponTypes.knife => weaponFeatures.fireRate,
        WeaponTypes.p90 => weaponFeatures.fireRate,
        WeaponTypes.pistol => weaponFeatures.fireRate
    };

    internal float damage => weaponTypes switch
    {
        WeaponTypes.grenade => weaponFeatures.damage,
        WeaponTypes.hammer => weaponFeatures.damage,
        WeaponTypes.knife => weaponFeatures.damage,
        WeaponTypes.p90 => weaponFeatures.damage,
        WeaponTypes.pistol => weaponFeatures.damage
    };
}

public struct WeaponFeatures
{
    public float fireRate;
    public float damage;
}

