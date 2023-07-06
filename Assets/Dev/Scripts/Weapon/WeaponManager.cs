using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponFeatures weaponFeatures;
    [SerializeField] private WeaponTypes weaponTypes;

    internal WeaponType type => new(weaponTypes, weaponFeatures);
    
}

public enum WeaponTypes{grenade, hammer, knife, p90, pistol}

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

[System.Serializable]
public struct WeaponFeatures
{
    public float fireRate;
    public float damage;
}
