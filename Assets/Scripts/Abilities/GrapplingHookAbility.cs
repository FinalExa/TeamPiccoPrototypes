using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAbility : AbilityProjectileBody
{
    
    
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
        base.DestroyProjectile(hitEnemy);
    }

    public override void AbilityEffectAfterDuration()
    {
        base.AbilityEffectAfterDuration();
    }
}
