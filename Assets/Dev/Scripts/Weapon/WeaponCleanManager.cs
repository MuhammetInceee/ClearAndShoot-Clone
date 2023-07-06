using System;
using PaintIn3D;
using UnityEngine;

public class WeaponCleanManager : MonoBehaviour
{
    [SerializeField] private P3dChannelCounter counter;

    private void Update()
    {
        if (counter.RatioA >= 0.9f)
        {
            print("Clean");
        }
    }
}
