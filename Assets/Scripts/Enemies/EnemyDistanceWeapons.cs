using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceWeapons : MonoBehaviour
{
    [SerializeField] private bool distanceWeaponsEnabled;
    private WeaponCycle weaponCycle;
    private EnemyPattern enemyPattern;
    private PlayerReferences playerRef;
    private float distance;
    [SerializeField] private float distanceValue;

    private void Awake()
    {
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
        enemyPattern = this.gameObject.GetComponent<EnemyPattern>();
        playerRef = FindObjectOfType<PlayerReferences>();
    }

    private void Update()
    {
        if (enemyPattern.alerted && distanceWeaponsEnabled) DistanceWeapons();
    }

    private void DistanceWeapons()
    {
        Vector3 thisPos = this.gameObject.transform.position;
        Vector3 playerPos = playerRef.gameObject.transform.position;
        distance = Vector3.Distance(thisPos, playerPos);
        if (distance > distanceValue && weaponCycle.activeWeaponIndex != 0) weaponCycle.activeWeaponIndex = 0;
        else if (weaponCycle.activeWeaponIndex != 1) weaponCycle.activeWeaponIndex = 1;
    }
}
