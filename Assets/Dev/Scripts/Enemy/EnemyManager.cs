using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private const string MaterialSlierName = "_SplitPosition";
    
    [SerializeField] private float health;
    [SerializeField] private SkinnedMeshRenderer character;
    
    private Animator _animator;
    private Material _material;
    private bool _canHit = true;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _material = character.material;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Bullet bullet) || !_canHit) return;
        
        GetHit(10);
        bullet.GetBackPool();
        
        if (health <= 0) Murdered();
    }
    
    private void GetHit(float splitPosition)
    {
        
        health -= splitPosition;
        _material.SetFloat(MaterialSlierName, health / 100);
    }

    private void Murdered()
    {
        _canHit = false;
        _animator.enabled = false;
    }
}
