using UnityEngine;

public class SealedGate : GateBase
{
    [SerializeField] private int sealHealth;
    [SerializeField] private GateLock gateLock;

    protected override void BulletTrigger(Collider other, Bullet bullet)
    {
        base.BulletTrigger(other, bullet);
        if (sealHealth > 0)
        {
            sealHealth--;
            if (sealHealth <= 0)
            {
                gateLock.Unlocked();
            }
        }
        else
        {
            buffValue += bullet.damage;
        }
        
        GateUpdater();
    }
}
