using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    private PlayerReferences playerRef;
    private float meleeDuration;
    private float meleeCooldown;
    [SerializeField] GameObject meleeObject;
    private bool meleeOn;
    private bool meleeCooldownOn;
    private float timerAttack;
    private float timerCooldown;

    private void Awake()
    {
        playerRef = this.GetComponent<PlayerReferences>();
    }

    private void Start()
    {
        meleeObject.SetActive(false);
        timerAttack = meleeDuration;
        timerCooldown = meleeCooldown;
        
    }

    private void Update()
    {
        MeleeCheck();
        MeleeActive();
        MeleeCooldown();
        meleeDuration = playerRef.playerData.meleeDuration;
        meleeCooldown = playerRef.playerData.meleeCooldown;
    }

    private void MeleeCheck()
    {
        if (playerRef.playerInputs.LeftClickInput == true && !meleeCooldownOn)
        {
            meleeObject.SetActive(true);
            meleeOn = true;
        }
    }

    private void MeleeActive()
    {
        if (meleeOn)
        {
            if (timerAttack > 0) timerAttack -= Time.deltaTime;
            else
            {
                meleeObject.SetActive(false);
                timerAttack = meleeDuration;
                meleeOn = false;
                meleeCooldownOn = true;
            }
        }
    }

    private void MeleeCooldown()
    {
        if (meleeCooldownOn)
        {
            if (timerCooldown > 0) timerCooldown -= Time.deltaTime;
            else
            {
                timerCooldown = meleeCooldown;
                meleeCooldownOn = false;
            }
        }
    }
}
