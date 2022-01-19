using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionRange : MonoBehaviour
{
    [SerializeField] private EnemyPattern enemyPattern;
    [SerializeField] private float detectionRange;
    [SerializeField] private Collider[] colliders;
    private bool detected;

    private void Start()
    {
        detected = false;
    }

    private void FixedUpdate()
    {
        if (enemyPattern.canShootAtPlayer && !detected)
        {
            colliders = Physics.OverlapSphere(this.transform.position, detectionRange);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Player"))
                {
                    enemyPattern.alerted = true;
                    detected = true;
                    break;
                }
            }
        }
    }
}
