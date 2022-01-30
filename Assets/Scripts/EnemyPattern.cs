using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPattern : MonoBehaviour
{
    public bool alerted;
    private Shoot shoot;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float closenessFromPlayer;
    [SerializeField] private GameObject projectileStartPos;
    private PlayerReferences playerRef;
    private NavMeshAgent thisNavMesh;
    [HideInInspector] public bool canShootAtPlayer;

    private void Awake()
    {
        shoot = this.gameObject.GetComponent<Shoot>();
        playerRef = FindObjectOfType<PlayerReferences>();
        thisNavMesh = this.gameObject.GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        alerted = false;
        shoot.enabled = false;
        canShootAtPlayer = false;
    }

    private void Update()
    {
        Alert();
    }

    private void FixedUpdate()
    {
        canShootAtPlayer = CheckOcclusionWithPlayer();
    }

    private void Alert()
    {
        if (alerted && thisNavMesh.enabled)
        {
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
        Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
        Vector3 direction = (playerRef.transform.position - startPos).normalized;
        bool canSeePlayer = false;
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                canSeePlayer = true;
                thisNavMesh.enabled = true;
                shoot.enabled = true;
            }
            else if (hit.collider.gameObject.CompareTag("Laser"))
            {
                thisNavMesh.enabled = false;
                shoot.enabled = false;
            }
            else
            {
                thisNavMesh.enabled = true;
                shoot.enabled = false;
            }
        }
        return canSeePlayer;
    }
}
