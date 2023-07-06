using UnityEngine;

public class SealedGate : GateBase
{
    [SerializeField] private int sealHealth;
    [SerializeField] private GameObject seal;

    protected override void BulletTrigger(Collider other, Bullet bullet)
    {
        base.BulletTrigger(other, bullet);
        if (sealHealth > 0)
        {
            sealHealth--;
            if (sealHealth <= 0)
            {
                seal.SetActive(false);
            }
        }
        else
        {
            buffValue += bullet.damage;
        }
        
        GateUpdater();
    }
}
