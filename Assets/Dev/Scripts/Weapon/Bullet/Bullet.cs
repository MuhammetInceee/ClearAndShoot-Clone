using System;
using UnityEngine;
using UnityEngine.Serialization;

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

public class Bullet : MonoBehaviour
{
    private const float BulletLifeTime = 2;
    
    internal float damage;
    
    [SerializeField] private BulletTypes bulletTypes;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Rigidbody rb;
    
    private float _startTime;
    private Transform _parent;
    private Quaternion _deltaRot;
    private BulletType _bulletType => new BulletType(bulletTypes, rb, _deltaRot);
    
    private void OnEnable()
    {
        if (_parent == null) _parent = transform.parent;
        _startTime = Time.time;
    }
    private void FixedUpdate()
    {
        _bulletType.fixedUpdate.Invoke();
        if (Time.time >= _startTime + BulletLifeTime) GetBackPool();
    }
    internal void GetBackPool()
    {
        var transform1 = transform;
        if(trail != null) trail.Clear();
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
