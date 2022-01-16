using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectile : Projectile
{

    public override void Update()
    {
        base.Update();
    }

    public void StopAtTargetLocation()
    {
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            AbilityProjectileBody child = thisProjectileChildren[i].GetComponent<AbilityProjectileBody>();
            child.stopAtTarget = true;
            child.target = target;
        }
    }
}
