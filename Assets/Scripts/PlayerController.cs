using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerReferences playerReferences;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public string curState;
    [HideInInspector] [Range(0, 100)] public int battery;
    [SerializeField] private Text batteryText;

    private void Awake()
    {
        Projectile.absorb += BatteryUpdate;
        playerReferences = this.gameObject.GetComponent<PlayerReferences>();
    }

    private void Start()
    {
        battery = 0;
        BatteryUpdate(0);
    }

    public void BatteryUpdate(int value)
    {
        battery += value;
        battery = Mathf.Clamp(battery, 0, 100);
        batteryText.text = battery + "%";
    }
}
