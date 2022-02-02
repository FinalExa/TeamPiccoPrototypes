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
    private float intermittentTimer;// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * TurbineRotationSpeed * Time.deltaTime);
    }
}
