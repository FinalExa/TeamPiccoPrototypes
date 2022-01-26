using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackHoleAbility : AbilityProjectileBody
{
    [SerializeField] private float blackHoleRadius;
    [SerializeField] private float blackHolePullSpeed;
    [SerializeField] private GameObject rangeObj;
    private bool isActive;
    private Collider[] hits;

    public override void AbilityEffectDuration()
    {
        ActivateRange();
        BlackHoleEffect(this.transform.position, blackHoleRadius);
    }

    public override void AbilityEffectAfterDuration()
    {
        BlackHoleEffectStop();
        DeactivateRange();
        DestroyProjectile();
    }

    private void BlackHoleEffect(Vector3 position, float radius)
    {
        hits = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                Vector3 direction = (this.gameObject.transform.position - hits[i].gameObject.transform.position).normalized;
                hits[i].gameObject.GetComponent<EnemyPattern>().enabled = false;
                hits[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                hits[i].gameObject.GetComponent<Shoot>().enabled = true;
                hits[i].gameObject.transform.Translate(Time.fixedDeltaTime * blackHolePullSpeed * direction, Space.World);
            }
        }
    }

    private void BlackHoleEffectStop()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                hits[i].gameObject.GetComponent<EnemyPattern>().enabled = true;
                hits[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    public override void MainFireInteraction()
    {
    }

    private void ActivateRange()
    {
        if (!isActive)
        {
            isActive = true;
            rangeObj.transform.localScale = new Vector3(blackHoleRadius, blackHoleRadius, blackHoleRadius);
            rangeObj.SetActive(true);
        }
    }

    private void DeactivateRange()
    {
        rangeObj.SetActive(false);
        isActive = false;
    }
}
