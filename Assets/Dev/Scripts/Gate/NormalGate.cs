using UnityEngine;

public class NormalGate : GateBase
{
    protected override void BulletTrigger(Collider other, Bullet bullet)
    {
        base.BulletTrigger(other, bullet);

        buffValue += bullet.damage;
        GateUpdater();
    }
}
