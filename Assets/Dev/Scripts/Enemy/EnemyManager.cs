using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float health;
    
    private Animator _animator;
    private bool _canHit = true;
    
    private void Awake() => _animator = GetComponent<Animator>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Bullet bullet) || !_canHit) return;
        
        health -= 10;
        bullet.GetBackPool();
        
        if (health <= 0) Murdered();
    }

    private void Murdered()
    {
        _canHit = false;
        _animator.enabled = false;
    }
}
