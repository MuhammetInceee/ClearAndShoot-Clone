using System;
using UnityEngine;
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

public class Bullet : MonoBehaviour
{
    private const float BulletLifeTime = 2;
    
    internal float damage;
    
    private float _startTime;
    private Transform _parent;
    private BulletType _bulletType;
    private TrailRenderer _trail;
    private Rigidbody _rb;
    private Quaternion _deltaRot;
    
    [SerializeField] private BulletTypes bulletTypes;
    
    private void OnEnable()
    {
        _bulletType = new BulletType(bulletTypes, _rb, _deltaRot);
        _rb = GetComponent<Rigidbody>();
        if (_parent == null) _parent = transform.parent;
        _startTime = Time.time;
        _trail = transform.GetComponentInChildren<TrailRenderer>();
    }
    private void FixedUpdate()
    {
        _bulletType.fixedUpdate.Invoke();
        if (Time.time >= _startTime + BulletLifeTime) GetBackPool();
    }
    internal void GetBackPool()
    {
        var transform1 = transform;
        if(_trail != null) _trail.Clear();
        gameObject.SetActive(false);
        transform1.SetParent(_parent);
        damage = 0;
    }
}

public enum BulletTypes{rotateable, stuck}

public class BulletType
{
    internal static readonly BulletType rotateable = new(BulletTypes.rotateable, null, Quaternion.identity);
    internal static readonly BulletType stuck = new(BulletTypes.stuck, null, Quaternion.identity);

    private readonly BulletTypes bulletTypes;
    private readonly Rigidbody rb;
    private Quaternion deltaRot;

    internal BulletType(BulletTypes bulletTypes, Rigidbody rb, Quaternion deltaRot)
    {
        this.bulletTypes = bulletTypes;
        this.rb = rb;
        this.deltaRot = deltaRot;
    }

    internal Action fixedUpdate => bulletTypes switch
    {
        BulletTypes.rotateable => () =>
        {
            deltaRot = Quaternion.Euler(Vector3.right * (1000 * Time.deltaTime));
            rb.MovePosition(rb.position + Vector3.forward * (5 * Time.deltaTime));
            rb.MoveRotation(rb.rotation * deltaRot);
        },
        
        BulletTypes.stuck => () =>
        {
            rb.MovePosition(rb.position + Vector3.forward * (5 * Time.deltaTime));
        } 
    };
}
