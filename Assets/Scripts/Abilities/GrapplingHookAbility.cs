using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAbility : AbilityProjectileBody
{
    private PlayerReferences PlayerRef;

    private void Awake()
    {
        PlayerRef = FindObjectOfType<PlayerReferences>();
    }

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
        PlayerRef.gameObject.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

        base.DestroyProjectile(hitEnemy);
    }

    public override void AbilityEffectAfterDuration()
    {
        base.AbilityEffectAfterDuration();
    }
}
