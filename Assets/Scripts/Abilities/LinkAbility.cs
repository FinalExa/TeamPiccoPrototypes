using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAbility : AbilityProjectile
{
    [SerializeField] private float linkRadius;
    [SerializeField] private GameObject rangeObj;
    private bool isActive;
    private Collider[] hits;

    public override void Start()
    {
        base.Start();
    }

    public override void AbilityEffectDuration()
    {
        ActivateRange(linkRadius);
        LinkEffect(this.transform.position, linkRadius);
    }

    private void LinkEffect(Vector3 position, float radius)
    {
        hits = Physics.OverlapSphere(position, radius);
    }

    public override void MainFireInteraction()
    {

    }

    private void ActivateRange(float range)
    {
        if (!isActive)
        {
            isActive = true;
        }
        else
        {
            rangeObj.transform.localScale = new Vector3(range, range, range);
            rangeObj.SetActive(true);
        }
    }
    private void DeactivateRange()
    {
        rangeObj.SetActive(false);
        isActive = false;
    }
}
