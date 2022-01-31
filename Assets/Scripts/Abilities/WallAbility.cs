using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAbility : AbilityProjectileBody
{
    private bool reachingTarget;
    private Vector3 startingPos;
    //private SecondaryAbility myScript;
    public override void Start()
    {
        base.Start();
        reachingTarget = true;
    }

    public override void AbilityEffectDuration()
    {
        if (reachingTarget)
        {
            reachingTarget = false;
            startingPos = this.transform.position;
        }
        else
        {
            this.transform.position = startingPos;
        }
    }

    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile(false);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Enemy") && reachingTarget)
        {
            other.GetComponent<Health>().DecreaseHP(damage);
            thisProjectileRigidbody.velocity = Vector3.zero;
        }
    }
}
