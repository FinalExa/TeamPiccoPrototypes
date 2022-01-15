using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    private WeaponStatTracker weaponUsed;
    private int activeWeaponIndex;
    private bool lockUntilNextClick;
    private PlayerReferences playerRef;
    [SerializeField] private bool autoInput;
    [SerializeField] private bool autoFirstShootStop;
    [SerializeField] private GameObject projectileStartPosition;
    [SerializeField] private GameObject reloadCanvas;
    [SerializeField] private Text ammoText;

    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
    }

    private void Start()
    {
        activeWeaponIndex = 0;
        if (!autoInput)
        {
            for (int i = 0; i < weaponUsed.weaponInfo.Length; i++)
            {
                weaponUsed.weaponInfo[i].shootReady = true;
            }
        }
        else autoFirstShootStop = true;
    }

    private void Update()
    {
        if (!autoInput) InputCheck();
        if (lockUntilNextClick) LockUntilNextClick();
        WeaponChange();
        if (playerRef.playerInputs.RKey == true && weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent != weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize) weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = true;
        UpdateText();
    }

    private void FixedUpdate()
    {
        if (!weaponUsed.weaponInfo[activeWeaponIndex].shootReady) ShootTimer();
        if (weaponUsed.weaponInfo[activeWeaponIndex].isRecharging) RechargeTimer();
    }

    private void LockUntilNextClick()
    {
        if (playerRef.playerInputs.LeftClickInput == false)
        {
            lockUntilNextClick = false;
        }
    }

    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    private void ShootTimer()
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer == weaponUsed.weaponInfo[activeWeaponIndex].weapon.shootDelay && autoInput)
        {
            if (autoFirstShootStop) autoFirstShootStop = false;
            else ProjectileSpawn();
        }
        if (weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer > 0) weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer -= Time.fixedDeltaTime;
        else
        {
            weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.shootDelay;
            if (!autoInput) weaponUsed.weaponInfo[activeWeaponIndex].shootReady = true;
        }
    }

    private void RechargeTimer()
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer == weaponUsed.weaponInfo[activeWeaponIndex].weapon.rechargeDelay && autoInput) ProjectileSpawn();
        if (weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer > 0)
        {
            weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer -= Time.fixedDeltaTime;
        }
        else
        {
            weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.rechargeDelay;
            if (weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent >= weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize)
            {
                if (weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent == 0)
                {
                    weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent = weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize;
                    if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.endlessAmmo) weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent -= weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize;
                }
                else
                {
                    int ammoPartial = weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize - weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent;
                    weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent += ammoPartial;
                    if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.endlessAmmo) weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent -= ammoPartial;
                }
            }
            else if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.endlessAmmo)
            {
                weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent = weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent;
                weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent = 0;
            }
            if (!autoInput)
            {
                weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = false;
            }
        }
    }

    private void ProjectileSpawn()
    {
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunWeapon) ProjectileCreate(false);
        else
        {
            for (int i = 0; i < weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunProjectileNumber; i++)
            {
                if (i == 1)
                {
                    ProjectileCreate(false);
                }
                else
                {
                    float spread = Random.Range(0, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread * 2) - weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread;
                    ProjectileCreate(true);
                }
            }
        }
    }

    private void ProjectileCreate(bool isShotgun)
    {
        GameObject projectile = Instantiate(weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (!autoInput)
        {
            if (!isShotgun) proj.target = playerRef.mousePos.VectorPointToShoot;
            else proj.target = playerRef.mousePos.VectorPointToShoot + new Vector3(Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread), 0, Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread));
        }
        else
        {
            if (!isShotgun) proj.target = playerRef.gameObject.transform.position;
            else proj.target = playerRef.gameObject.transform.position + new Vector3(Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread), 0, Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread));
        }
        proj.damage = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileDamage;
        if (!isShotgun) proj.speed = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileSpeed;
        else proj.speed = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileSpeed - Random.Range(0, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpeedDifference);
    }

    private void InputCheck()
    {
        if (playerRef.playerInputs.LeftClickInput == true && weaponUsed.weaponInfo[activeWeaponIndex].shootReady && !weaponUsed.weaponInfo[activeWeaponIndex].isRecharging && weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent > 0 && !lockUntilNextClick)
        {
            weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent--;
            ProjectileSpawn();
            if (weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent == 0)
            {
                weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = true;
            }
            else weaponUsed.weaponInfo[activeWeaponIndex].shootReady = false;
            if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.automaticWeapon) lockUntilNextClick = true;
        }
    }

    private void UpdateText()
    {
        if (!autoInput)
        {
            ammoText.text = "Ammo: " + weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent + "/" + weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent;
            reloadCanvas.SetActive(weaponUsed.weaponInfo[activeWeaponIndex].isRecharging);
        }
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
