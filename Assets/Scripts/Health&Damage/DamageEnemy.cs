using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : DamageTaken
{
    [HideInInspector] public Vector3 startingPos;
    private EnemyPattern enemyPattern;
    protected override void Awake()
    {
        base.Awake();
        enemyPattern = this.gameObject.GetComponent<EnemyPattern>();
    }
    protected override void Start()
    {
        base.Start();
        startingPos = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag(hostileTag) && !other.gameObject.CompareTag("Player")))
        {
            thisRB.velocity = Vector3.zero;
            if (!other.gameObject.CompareTag("Melee")) Destroy(other.gameObject);
            if (!isDamaged)
            {
                //enemyPattern.alerted = true;
                TakeDamage();
            }

        }

    }
}
