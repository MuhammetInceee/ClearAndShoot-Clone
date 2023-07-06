using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private P3dHitBetween _hitBetween;
    
    void Start()
    {
        _hitBetween = GetComponent<P3dHitBetween>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(_hitBetween.HitObj.name);
        }
    }
}
