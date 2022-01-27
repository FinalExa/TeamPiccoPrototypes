using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectile : Projectile
{
    [HideInInspector] public float stopTime;

    public override void Update()
    {
        base.Update();
    }

    public void StopAfterTime()
    {
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            AbilityProjectileBody child = thisProjectileChildren[i].GetComponent<AbilityProjectileBody>();
            child.stopAfterTime = true;
            child.stopTime = stopTime;
        }
    }

    public void StopAtTargetLocation()
    {
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            AbilityProjectileBody child = thisProjectileChildren[i].GetComponent<AbilityProjectileBody>();
            child.stopAtTarget = true;
        }
    }
    public void SetDurationInfos(bool usesDuration, float durationTimeValue, bool stopsPlayer, bool interactionWithMainShots, GameObject originPoint)
    {
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            AbilityProjectileBody child = thisProjectileChildren[i].GetComponent<AbilityProjectileBody>();
            child.SetDurationTimer(usesDuration, durationTimeValue);
            child.stopsPlayer = stopsPlayer;
            child.interactsWithMainShots = interactionWithMainShots;
            child.originPoint = originPoint;
            child.target = target;
        }
    }
}
