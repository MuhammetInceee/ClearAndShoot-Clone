using TMPro;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private MeshRenderer levelPlate;
    [SerializeField] private TextMeshPro levelText;

    private void Start()
    {
        CheckAvailable();
        InitVariables();
    }

    private void InitVariables()
    {
        levelText.text = $"LVL {weaponManager.weaponLevel}";
    }


    public void CheckAvailable()
    {
        levelPlate.material.color = weaponManager.Stackable() ? Color.green : Color.red;
    }
}
