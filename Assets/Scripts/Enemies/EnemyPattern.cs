using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour
{
    public bool alerted;
    private bool alertedFirstFramePassed;
    private bool alertedFirstFrameCanvasActive;
    [SerializeField] private GameObject alertedCanvas;
    [SerializeField] private float alertedCanvasActiveDuration;
    private float alertedCanvasDurationTimer;
    private Shoot shoot;
    [SerializeField] private float rangeToSpotPlayer;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private GameObject projectileStartPos;
    private PlayerReferences playerRef;
    private NavMeshAgent thisNavMesh;
    [HideInInspector] public bool canShootAtPlayer;
    [SerializeField] private bool canAlertNearbyEnemies;
    [SerializeField] private bool alertOthersWorksThroughWalls;
    [HideInInspector] public bool canAlert;
    [SerializeField] private float alertNearbyEnemiesRange;
    [HideInInspector] public float currentDistanceFromPlayer;
    private bool waitBeforeSearchingAgain;
    private bool lockSearch;
    [SerializeField] private float waitBeforeSearchTime;
    private float waitBeforeSearchTimer;

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
        alertedCanvasDurationTimer = alertedCanvasActiveDuration;
        waitBeforeSearchTimer = waitBeforeSearchTime;
        if (canAlertNearbyEnemies) canAlert = true;
    }

    private void Update()
    {
        currentDistanceFromPlayer = Vector3.Distance(playerRef.transform.position, this.transform.position);
        canShootAtPlayer = CheckOcclusionWithPlayer();
        CheckForPlayerInRange();
        Alert();
        AlertedCanvas();
        if (waitBeforeSearchingAgain) WaitBeforeSearchingAgain();
    }

    private void CheckForPlayerInRange()
    {
        if (currentDistanceFromPlayer <= rangeToSpotPlayer && !alerted && canShootAtPlayer) alerted = true;
    }

    private void Alert()
    {
        if (alerted && thisNavMesh.enabled)
        {
            if (!alertedFirstFramePassed) AlertedFirstFrame();
            if (canAlert && canAlertNearbyEnemies) AlertNearbyEnemies();
            if (canShootAtPlayer)
            {
                if (waitBeforeSearchingAgain) StopWaitingBeforeSearching();
                if (lockSearch) lockSearch = false;
                shoot.enabled = true;
                if (currentDistanceFromPlayer <= distanceFromPlayer)
                {
                    thisNavMesh.isStopped = false;
                    thisNavMesh.SetDestination((playerRef.transform.position - this.transform.position).normalized * (distanceFromPlayer - currentDistanceFromPlayer));
                }
                else
                {
                    thisNavMesh.isStopped = false;
                    thisNavMesh.SetDestination(playerRef.transform.position);
                }
            }
            else
            {
                if (!lockSearch)
                {
                    shoot.enabled = true;
                    thisNavMesh.isStopped = true;
                    waitBeforeSearchingAgain = true;
                }
            }
        }
    }

    private void WaitBeforeSearchingAgain()
    {
        if (waitBeforeSearchTimer > 0) waitBeforeSearchTimer -= Time.deltaTime;
        else
        {
            lockSearch = true;
            shoot.enabled = false;
            StopWaitingBeforeSearching();
            thisNavMesh.SetDestination(playerRef.transform.position);
        }
    }

    private void StopWaitingBeforeSearching()
    {
        thisNavMesh.isStopped = false;
        waitBeforeSearchTimer = waitBeforeSearchTime;
        waitBeforeSearchingAgain = false;
    }

    private bool CheckOcclusionWithPlayer()
    {
        Vector3 startPos = this.transform.position;
        Vector3 direction = (playerRef.transform.position - startPos).normalized;
        bool canSeePlayer = false;
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                canSeePlayer = true;
                thisNavMesh.enabled = true;
            }
            else if (hit.collider.gameObject.CompareTag("Laser")) thisNavMesh.enabled = false;
            else thisNavMesh.enabled = true;
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
                if (alertOthersWorksThroughWalls)
                {
                    enemyPattern.canAlert = false;
                    enemyPattern.alerted = true;
                }
                else
                {
                    Vector3 startPos = this.transform.position;
                    Vector3 direction = (enemyPattern.gameObject.transform.position - startPos).normalized;
                    if (Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
                    {
                        EnemyPattern hitObj = hit.collider.gameObject.GetComponent<EnemyPattern>();
                        if (hitObj == enemyPattern)
                        {
                            enemyPattern.canAlert = false;
                            enemyPattern.alerted = true;
                        }
                    }
                }
            }
        }
    }

    private void AlertedFirstFrame()
    {
        alertedFirstFrameCanvasActive = true;
        alertedCanvas.gameObject.SetActive(true);
        alertedFirstFramePassed = true;
    }

    private void AlertedCanvas()
    {
        if (alertedFirstFrameCanvasActive)
        {
            if (alertedCanvasDurationTimer > 0)
            {
                alertedCanvasDurationTimer -= Time.deltaTime;
            }
            else
            {
                alertedCanvas.gameObject.SetActive(false);
                alertedFirstFrameCanvasActive = false;
            }
        }
    }
}
