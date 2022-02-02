using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WallAbility : AbilityProjectile
{
    private bool reachingTarget;
    private Vector3 startingPos;
    private NavMeshAgent navMeshAgent;
    private float tempDamage;

    public override void Awake()
    {
        base.Awake();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void Start()
    {
        base.Start();
        tempDamage = damage;
        reachingTarget = true;
    }

    public override void AbilityEffectDuration()
    {
        WallAbilityEffect();
    }

    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Enemy") && reachingTarget)
        {
            other.GetComponent<Health>().DecreaseHP(damage);
            projectileRigidbody.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
        }
    }


    private void WallAbilityEffect()
    {
        if (reachingTarget)
        {
            reachingTarget = false;
            startingPos = this.transform.position;
        }
        else
        {
            this.transform.position = startingPos;
        }

        if (projectileRigidbody.velocity != Vector3.zero) damage = tempDamage;
        else damage = 0;
    }
}
