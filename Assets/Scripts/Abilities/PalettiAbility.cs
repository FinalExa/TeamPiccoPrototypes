using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalettiAbility : AbilityProjectile
{

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    public override void OnDeploy()
    {
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

    public override void DestroyProjectile()
    {
        projectileRigidbody.velocity = Vector3.zero;
    }
}
