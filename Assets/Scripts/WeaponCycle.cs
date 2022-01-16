using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycle : MonoBehaviour
{
    private WeaponStatTracker weaponUsed;
    private PlayerReferences playerRef;
    [HideInInspector] public int activeWeaponIndex;
    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
    }

    private void Start()
    {
        activeWeaponIndex = 0;
    }

    private void Update()
    {
        WeaponChange();
    }

    private void WeaponChange()
    {
        if (playerRef.playerInputs.MouseWheelUp == true)
        {
            if (activeWeaponIndex + 1 < weaponUsed.weaponInfo.Length) activeWeaponIndex++;
            else activeWeaponIndex = 0;
        }
        if (playerRef.playerInputs.MouseWheelDown == true)
        {
            if (activeWeaponIndex - 1 >= 0) activeWeaponIndex--;
            else activeWeaponIndex = weaponUsed.weaponInfo.Length - 1;
        }
    }
}
