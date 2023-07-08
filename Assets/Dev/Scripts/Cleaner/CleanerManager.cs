using PaintIn3D;
using UnityEngine;

public class CleanerManager : MonoBehaviour
{
    private P3dHitBetween _hitBetween;
    private P3dPaintDecal _paintDecal;

    public int currentCleanLevel;

    private void Start()
    {
        _hitBetween = GetComponent<P3dHitBetween>();
        _paintDecal = GetComponent<P3dPaintDecal>();
    }

    private void Update()
    {
        if (_hitBetween.HitObj.TryGetComponent(out WeaponManager weaponManager))
        {
            //TODO currentLevel have to connect with Incremental System Clear Value
            if (weaponManager.weaponLevel <= currentCleanLevel)
            {
                _paintDecal.Opacity = 1;
                weaponManager.IsClean();
            }
            else _paintDecal.Opacity = 0.01f;
        }
        else _paintDecal.Opacity = 1;
    }
}