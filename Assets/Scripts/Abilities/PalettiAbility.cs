using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalettiAbility : AbilityProjectileBody
{

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    public override void OnDeploy()
    {
        base.OnDeploy();
    }

    public override void AbilityEffectDuration()
    {
        base.AbilityEffectDuration();
    }

    public override void AbilityEffectAfterDuration()
    {
        GameObject.Destroy(this.gameObject);
    }

    public override void MainFireInteraction()
    {
        base.MainFireInteraction();
    }

    public override void DestroyProjectile(bool hitEnemy)
    {
        thisProjectileRigidbody.velocity = Vector3.zero;
    }
}
