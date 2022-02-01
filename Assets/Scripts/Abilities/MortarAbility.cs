using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAbility : AbilityProjectile
{
    [SerializeField] float mortarRadius;
    [SerializeField] private GameObject rangeObj;
    private bool isActive;

    public override void AbilityEffectDuration()
    {
        ActivateRange();
    }
    public override void AbilityEffectAfterDuration()
    {
        MortarEffect(this.gameObject.transform.position, mortarRadius);
        DeactivateRange();
        DestroyProjectile();
    }

    private void MortarEffect(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                hits[i].gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
        }
    }

    private void ActivateRange()
    {
        if (!isActive)
        {
            isActive = true;
            rangeObj.transform.localScale = new Vector3(mortarRadius, mortarRadius, mortarRadius);
            rangeObj.SetActive(true);
        }
    }

    private void DeactivateRange()
    {
        rangeObj.SetActive(false);
        isActive = false;
    }
}
