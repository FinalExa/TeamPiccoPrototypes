using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaLaserAbility : AbilityProjectileBody
{
    [SerializeField] private float timesTriggeredInDuration;
    private float timeOffset;
    private float timer;

    private void Start()
    {
        timeOffset = durationTime / timesTriggeredInDuration;
        timer = timeOffset;
    }

    public override void AbilityEffectDuration()
    {
        if (timer > 0) timer -= Time.fixedDeltaTime;
        else
        {
            ShootRay();
            timer = timeOffset;
        }
    }

    private void ShootRay()
    {
        Vector3 rayOrigin = originPoint.transform.position;
        Vector3 direction = (target - rayOrigin).normalized;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, direction, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
        }
        Debug.DrawRay(rayOrigin, (hit.point - rayOrigin).normalized * Vector3.Distance(rayOrigin, hit.point), Color.red, timeOffset);
    }

    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }
}
