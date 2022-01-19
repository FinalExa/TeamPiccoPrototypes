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
        Vector3 newTarget = new Vector3(target.x, 0f, target.z);
        Vector3 rayOrigin = originPoint.transform.position;
        Vector3 direction = (newTarget - rayOrigin).normalized;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(rayOrigin, direction, Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
            Debug.DrawRay(rayOrigin, direction * Vector3.Distance(rayOrigin, hit.point), Color.red, timeOffset);
        }
    }
    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }
}