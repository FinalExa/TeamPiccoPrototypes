using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAbility : AbilityProjectileBody
{
    private PlayerReferences playerRef;

    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
    }

    public override void OnDeploy()
    {
        base.OnDeploy();
    }

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    public override void DestroyProjectile()
    {
        playerRef.gameObject.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        base.DestroyProjectile();
    }

    public override void AbilityEffectAfterDuration()
    {
        base.AbilityEffectAfterDuration();
    }
}
