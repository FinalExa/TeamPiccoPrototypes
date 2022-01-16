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
    public void SetDurationInfos(bool usesDuration, float durationTimeValue, bool afterTimer, bool stopsPlayer, bool interactionWithMainShots)
    {
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            AbilityProjectileBody child = thisProjectileChildren[i].GetComponent<AbilityProjectileBody>();
            child.SetDurationTimer(usesDuration, durationTimeValue);
            child.afterDuration = afterTimer;
            child.stopsPlayer = stopsPlayer;
            child.interactsWithMainShots = interactionWithMainShots;
        }
    }
}
