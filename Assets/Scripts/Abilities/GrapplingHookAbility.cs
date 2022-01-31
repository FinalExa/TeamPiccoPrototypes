using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAbility : AbilityProjectileBody
{
    public Transform ThisTransform;
    public Transform PlayerTransform;
    public float PullSpeed;

    
    public override void OnDeploy()
    {
        base.OnDeploy();
    }
    
    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();


    }

    public override void DestroyProjectile(bool hitEnemy)
    {
        if (hitEnemy)
        {
            thisProjectileRigidbody.velocity = Vector3.zero;
            thisProjectileRigidbody.angularVelocity = Vector3.zero;
            thisProjectileRigidbody.Sleep();

            PlayerTransform.Translate(ThisTransform.position * Time.deltaTime * PullSpeed);
        }

        base.DestroyProjectile(hitEnemy);
    }

    public override void AbilityEffectAfterDuration()
    {
        base.AbilityEffectAfterDuration();
    }
}
