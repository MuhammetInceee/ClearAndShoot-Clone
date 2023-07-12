using UnityEngine;

public class NormalGate : GateBase
{
    protected override void BulletTrigger(Collider other, Bullet bullet)
    {
        base.BulletTrigger(other, bullet);

        buffValue += bullet.damage / 100;
        GateUpdater();
    }

    protected override void GateHit(PlayerCollision playerCollision)
    {
        base.GateHit(playerCollision);
        
        playerCollision.GateHitSuccessful(buffTypes, buffValue);
        gameObject.SetActive(false);
    }
}
