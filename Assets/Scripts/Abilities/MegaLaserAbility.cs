using System.Linq;
using UnityEngine;

public class MegaLaserAbility : AbilityProjectile
{
    [SerializeField] private float timesTriggeredInDuration;
    private float timeOffset;
    private float timer;
    private Vector3 startPos;

    public override void Start()
    {
        base.Start();
        timeOffset = durationTime / timesTriggeredInDuration;
        timer = timeOffset;
        startPos = this.gameObject.transform.position;
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
        Vector3 rayOrigin = startPos;
        Vector3 positionOffset = startPos - originPoint.transform.position;
        Vector3 newTarget = new Vector3(target.x + positionOffset.x, 0f, target.z + positionOffset.z);
        Vector3 direction = (newTarget - rayOrigin).normalized;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(rayOrigin, direction, Mathf.Infinity);
        hits = hits.OrderBy((d) => (d.point - rayOrigin).sqrMagnitude).ToArray();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
            Debug.DrawRay(rayOrigin, direction * Vector3.Distance(rayOrigin, hit.point), Color.red, timeOffset);
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                break;
            }
        }
    }
    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }
}