using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour
{
    public bool alerted;
    private Shoot shoot;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private GameObject projectileStartPos;
    private PlayerReferences playerRef;
    private NavMeshAgent thisNavMesh;
    [HideInInspector] public bool canShootAtPlayer;
    [SerializeField] private bool canAlertNearbyEnemies;
    [HideInInspector] public bool canAlert;
    [SerializeField] private float alertNearbyEnemiesRange;

    private void Awake()
    {
        shoot = this.gameObject.GetComponent<Shoot>();
        playerRef = FindObjectOfType<PlayerReferences>();
        thisNavMesh = this.gameObject.GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        shoot.enabled = false;
        canShootAtPlayer = false;
        if (canAlertNearbyEnemies) canAlert = true;
    }

    private void Update()
    {
        canShootAtPlayer = CheckOcclusionWithPlayer();
        Alert();
    }

    private void Alert()
    {
        if (alerted && thisNavMesh.enabled)
        {
            if (canAlert && canAlertNearbyEnemies) AlertNearbyEnemies();
            if (canShootAtPlayer)
            {
                shoot.enabled = true;
                float distance = Vector3.Distance(playerRef.transform.position, this.transform.position);
                if (distance <= distanceFromPlayer)
                {
                    thisNavMesh.isStopped = false;
                    thisNavMesh.SetDestination(this.transform.forward - playerRef.transform.position);
                }
                else
                {
                    thisNavMesh.isStopped = false;
                    thisNavMesh.SetDestination(playerRef.transform.position);
                }
            }
            else
            {
                shoot.enabled = false;
                thisNavMesh.SetDestination(playerRef.transform.position);
            }
        }
    }

    private bool CheckOcclusionWithPlayer()
    {
        Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 direction = (playerRef.transform.position - startPos).normalized;
        bool canSeePlayer = false;
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                canSeePlayer = true;
                thisNavMesh.enabled = true;
            }
            else if (hit.collider.gameObject.CompareTag("Laser"))
            {
                thisNavMesh.enabled = false;
            }
            else
            {
                thisNavMesh.enabled = true;
            }
        }
        return canSeePlayer;
    }

    private void AlertNearbyEnemies()
    {
        canAlert = false;
        Collider[] enemies = Physics.OverlapSphere(this.transform.position, alertNearbyEnemiesRange);
        foreach (Collider enemy in enemies)
        {
            EnemyPattern enemyPattern = enemy.GetComponent<EnemyPattern>();
            if (enemyPattern != null)
            {
                enemyPattern.canAlert = false;
                enemyPattern.alerted = true;
            }
        }
    }
}
