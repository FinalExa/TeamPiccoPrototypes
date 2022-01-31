using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAbility : AbilityProjectileBody
{
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
        base.DestroyProjectile(hitEnemy);
    }
}
