using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GateLock : MonoBehaviour
{
    public List<Rigidbody> lockedChainParts;
    public Rigidbody padlock;
    
    public void Unlocked()
    {
        foreach (var part in lockedChainParts)
        {
            part.constraints = RigidbodyConstraints.None;
        }

        padlock.isKinematic = false;
        padlock.AddTorque(new Vector3(Random.Range(-100,100),Random.Range(-100,100),Random.Range(-100,100)));
        padlock.AddExplosionForce(200,padlock.transform.position + Vector3.down,100,100);
    }
}
