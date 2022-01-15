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
    private PlayerReferences playerRef;
    private NavMeshAgent thisNavMesh;

    private void Awake()
    {
        shoot = this.gameObject.GetComponent<Shoot>();
        playerRef = FindObjectOfType<PlayerReferences>();
        thisNavMesh = this.gameObject.GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        alerted = false;
    }

    private void Update()
    {
        if (alerted)
        {
            shoot.enabled = true;
            float distance = Vector3.Distance(playerRef.transform.position, this.transform.position);
            if (distance <= distanceFromPlayer)
            {
                if (distance < distanceFromPlayer - closenessFromPlayer)
                {
                    thisNavMesh.isStopped = false;
                    thisNavMesh.SetDestination(this.transform.forward - playerRef.transform.position);
                }
                else thisNavMesh.isStopped = true;
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
            thisNavMesh.isStopped = true;
        }
    }
}
