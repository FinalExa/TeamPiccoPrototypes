using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineOptions : MonoBehaviour
{
    [SerializeField] private GameObject TurbineTrap;
    public float TurbineRotationSpeed = 50.0f;
    public float TurbineDamageToPlayer;
    public float TurbineDamageToEnemies;
    [SerializeField] private bool isIntermittent;
    [SerializeField] private bool intermittentStatus;
    [SerializeField] private float intermittentActiveCooldown;
    [SerializeField] private float intermittentDeactiveCooldown;
    private float intermittentTimer;
    void Start()
    {

    }

    void Update()
    {
        TurbineTrap.transform.Rotate(Vector3.down * TurbineRotationSpeed * Time.deltaTime);
    }
}
