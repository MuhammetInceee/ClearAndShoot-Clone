using UnityEngine;

public class SealedGate : GateBase
{
    [SerializeField] private int sealHealth;
    [SerializeField] private GateLock gateLock;

    private bool _isUnlocked;
    
    protected override void BulletTrigger(Collider other, Bullet bullet)
    {
        base.BulletTrigger(other, bullet);
        if (sealHealth > 0)
        {
            sealHealth--;
            if (sealHealth <= 0)
            {
                gateLock.Unlocked();
                _isUnlocked = true;
            }
        }
        else
        {
            buffValue += bullet.damage / 100;
        }
        
        GateUpdater();
    }

    protected override void GateHit(PlayerCollision playerCollision)
    {
        base.GateHit(playerCollision);
        if (_isUnlocked)
        {
            playerCollision.GateHitSuccessful(buffTypes, buffValue);
        }
        else
        {
            playerCollision.GateHitFail(gameObject);
        }
        
        gameObject.SetActive(false);
    }
}
