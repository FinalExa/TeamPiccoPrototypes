using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOptions : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    public float speed = 50.0f;
    public float laserDamageToPlayer;
    public float laserDamageToEnemies;
    [SerializeField] private bool isIntermittent;
    [SerializeField] private bool intermittentStatus;
    [SerializeField] private float intermittentActiveCooldown;
    [SerializeField] private float intermittentDeactiveCooldown;
    private float intermittentTimer;

    private void Start()
    {
        if (isIntermittent) IntermittentInfo();
    }

    private void Update()
    {
        if (isIntermittent) LaserIntermittence();
    }

    private void LaserIntermittence()
    {
        if (intermittentTimer > 0) intermittentTimer -= Time.deltaTime;
        else
        {
            UpdateLaserInfo();
        }
    }

    private void UpdateLaserInfo()
    {
        intermittentStatus = !intermittentStatus;
        IntermittentInfo();
    }

    private void IntermittentInfo()
    {
        laser.SetActive(intermittentStatus);
        if (intermittentStatus) intermittentTimer = intermittentActiveCooldown;
        else intermittentTimer = intermittentDeactiveCooldown;
    }

}
