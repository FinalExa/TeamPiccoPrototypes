using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAbility : AbilityProjectile
{
    private PlayerReferences playerRef;

    public override void Awake()
    {
        base.Awake();
        playerRef = FindObjectOfType<PlayerReferences>();
    }

    public override void OnDeploy()
    {
        if (hitSomething) GrappleToPoint();
        else DestroyProjectile();
    }

    public override void OnEnemyHit()
    {
        GrappleToPoint();
    }

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    private void GrappleToPoint()
    {
        playerRef.gameObject.transform.position = new Vector3(this.transform.position.x, playerRef.gameObject.transform.position.y, this.transform.position.z);
        DestroyProjectile();
    }
}
