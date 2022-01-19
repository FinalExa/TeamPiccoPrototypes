using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAbility : AbilityProjectileBody
{
    [SerializeField] float mortarRadius;
    private bool isActive;

    public override void AbilityEffectDuration()
    {
        if (!isActive) isActive = true;
    }
    public override void AbilityEffectAfterDuration()
    {
        Collider[] hits = Physics.OverlapSphere(this.gameObject.transform.position, mortarRadius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                hits[i].gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
        }
        DestroyProjectile();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        if (isActive)
        {
            Gizmos.DrawSphere(this.gameObject.transform.position, mortarRadius);
        }
    }
}
