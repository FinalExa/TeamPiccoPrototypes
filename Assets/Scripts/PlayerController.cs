using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerReferences playerReferences;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public string curState;
    [HideInInspector] public bool DashLocked { get; set; }
    private float dashTimer;
    [HideInInspector] public Vector3 lastDashDirection;

    private void Awake()
    {
        playerReferences = this.gameObject.GetComponent<PlayerReferences>();
    }

    private void Start()
    {
        dashTimer = playerReferences.playerData.dashCooldown;
    }

    private void Update()
    {
        DashCooldown();
        DashDirectionCheck();
    }

    public void DashCooldown()
    {
        if (DashLocked)
        {
            if (dashTimer > 0) dashTimer -= Time.deltaTime;
            else
            {
                dashTimer = playerReferences.playerData.dashCooldown;
                DashLocked = false;
            }
        }
    }

    public void DashDirectionCheck()
    {
        if (playerReferences.playerInputs.MovementInput != Vector3.zero && playerReferences.playerInputs.MovementInput != lastDashDirection)
        {
            lastDashDirection = playerReferences.playerInputs.MovementInput;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            if (other.gameObject.GetComponent<Drop>().falseAmmoTrueHealth)
            {
                playerReferences.health.FullyHeal();
            }
            else
            {
                playerReferences.shoot.RefullAmmo();
            }
            GameObject.Destroy(other.gameObject);
        }
    }
}
