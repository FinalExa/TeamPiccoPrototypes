using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PillarWall : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    public PillarsAbility wallStart;
    public PillarsAbility wallEnd;
    private float timerToDespawn;
    private float despawnTimer;

    private void Awake()
    {
        obstacle = this.gameObject.GetComponent<NavMeshObstacle>();
    }

    private void Start()
    {
        obstacle.size = this.transform.localScale;
        if (wallStart.durationTimer < wallEnd.durationTimer) timerToDespawn = wallStart.durationTimer;
        else timerToDespawn = wallEnd.durationTimer;
        despawnTimer = timerToDespawn;
    }

    private void Update()
    {
        if (despawnTimer > 0) despawnTimer -= Time.deltaTime;
        else
        {
            GameObject.Destroy(this.gameObject);
        }
        if (wallStart == null || wallEnd == null)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
