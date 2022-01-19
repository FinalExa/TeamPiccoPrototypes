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
        shoot.enabled = false;
    }

    private void Update()
    {
        Alert();
    }

    private void Alert()
    {
        if (alerted)
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
    }
}
