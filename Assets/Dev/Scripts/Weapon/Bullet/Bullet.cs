using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BulletLifeTime = 2;
    
    public float damage;

    private float _startTime;
    private Transform _parent;

    private void OnEnable()
    {
        if (_parent == null) _parent = transform.parent;
        _startTime = Time.time;
    }
    private void FixedUpdate()
    {
        var movement = Vector3.forward * (5 * Time.fixedDeltaTime);
        transform.Translate(movement);

        if (Time.time >= _startTime + BulletLifeTime) GetBackPool();
    }
    internal void GetBackPool()
    {
        var transform1 = transform;
        
        transform1.position = Vector3.zero;
        gameObject.SetActive(false);
        transform1.SetParent(_parent);
    }
}
