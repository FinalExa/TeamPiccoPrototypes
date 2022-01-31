using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackHoleAbility : AbilityProjectile
{
    [SerializeField] private float blackHoleRadius;
    [SerializeField] private float blackHoleRadiusWhileTravelling;
    [SerializeField] private float blackHolePullSpeed;
    [SerializeField] private float blackHoleOffsetAddPerEnemy;
    [SerializeField] private GameObject rangeObj;
    private float blackHoleOffsetAddTotal;
    private bool isActive;
    private Collider[] hits;
    private List<EnemyPattern> enemiesAbsorbed;

    public override void Start()
    {
        base.Start();
        enemiesAbsorbed = new List<EnemyPattern>();
        blackHoleOffsetAddTotal = 0f;
    }

    public override void AbilityEffectBeforeReachingTarget()
    {
        ActivateRange(blackHoleRadiusWhileTravelling + blackHoleOffsetAddTotal);
        BlackHoleEffect(this.transform.position, blackHoleRadiusWhileTravelling + blackHoleOffsetAddTotal);
    }
    public override void AbilityEffectDuration()
    {
        ActivateRange(blackHoleRadius + blackHoleOffsetAddTotal);
        BlackHoleEffect(this.transform.position, blackHoleRadius + blackHoleOffsetAddTotal);
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
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.GetComponent<EnemyPattern>() != null && !enemiesAbsorbed.Contains(hit.gameObject.GetComponent<EnemyPattern>()))
            {
                enemiesAbsorbed.Add(hit.gameObject.GetComponent<EnemyPattern>());
            }
        }
        blackHoleOffsetAddTotal = blackHoleOffsetAddPerEnemy * enemiesAbsorbed.Count;
    }

    private void BlackHoleEffectStop()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy") && hits[i] != null)
            {
                hits[i].gameObject.GetComponent<EnemyPattern>().enabled = true;
                hits[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
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
