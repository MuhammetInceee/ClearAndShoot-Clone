using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject cleaner;
        
    private P3dHitBetween _hitBetween;
    private P3dPaintDecal _paintDecal;

    public int currentCleanLevel;
    public List<GameObject> weaponList;
    
    public Transform firstTr;
    public float defX;
    public float defZ;


    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _hitBetween = cleaner.GetComponent<P3dHitBetween>();
        _paintDecal = cleaner.GetComponent<P3dPaintDecal>();
    }

    private void Update()
    {
        CleanerPressureController();
    }

    private void CleanerPressureController()
    {
        if (_hitBetween.HitObj.TryGetComponent(out WeaponManager weaponManager))
        {
            //TODO currentLevel have to connect with Incremental System Clear Value
            if (weaponManager.weaponLevel <= currentCleanLevel)
            {
                _paintDecal.Opacity = 1;
                weaponManager.Clear(this);
            }
            else _paintDecal.Opacity = 0.01f;
        }
        else _paintDecal.Opacity = 1;
    }
}