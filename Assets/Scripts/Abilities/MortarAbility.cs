using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAbility : AbilityProjectileBody
{
    [SerializeField] float mortarRadius;
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
}
