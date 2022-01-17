using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackHoleAbility : AbilityProjectileBody
{
    [SerializeField] float blackHoleRadius;
    [SerializeField] float blackHolePullSpeed;
    private Collider[] hits;

    public override void AbilityEffectDuration()
    {
        hits = Physics.OverlapSphere(this.gameObject.transform.position, blackHoleRadius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                Vector3 direction = (this.gameObject.transform.position - hits[i].gameObject.transform.position).normalized;
                hits[i].gameObject.GetComponent<EnemyPattern>().enabled = false;
                hits[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                hits[i].gameObject.transform.Translate(Time.fixedDeltaTime * blackHolePullSpeed * direction, Space.World);
            }
        }
    }

    public override void AbilityEffectAfterDuration()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                hits[i].gameObject.GetComponent<EnemyPattern>().enabled = false;
                hits[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        DestroyProjectile();
    }

    public override void MainFireInteraction()
    {
    }
}
