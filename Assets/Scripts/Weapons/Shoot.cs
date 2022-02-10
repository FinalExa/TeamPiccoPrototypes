using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    private WeaponStatTracker weaponUsed;
    private WeaponCycle weaponCycle;
    private int activeWeaponIndex;
    private bool clickInput;
    private Vector3 target;
    private bool lockUntilNextClick;
    private bool charging;
    private PlayerReferences playerRef;
    private float bulletDamage;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool autoFirstShootStop;
    [SerializeField] private GameObject projectileStartPosition;
    [SerializeField] private GameObject reloadCanvas;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text chargeText;

    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
    }

    private void Start()
    {
        for (int i = 0; i < weaponUsed.weaponInfo.Length; i++)
        {
            weaponUsed.weaponInfo[i].shootReady = true;
        }
        if (!isEnemy) chargeText.text = string.Empty;
    }

    private void Update()
    {
        activeWeaponIndex = weaponCycle.activeWeaponIndex;
        InputCheck();
        if (lockUntilNextClick) LockUntilNextClick();
        if (playerRef.playerInputs.RKey == true && weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent != weaponUsed.weaponInfo[activeWeaponIndex].weapon.magazineSize) weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = true;
        UpdateText();
        ChargeShot();
        if (!weaponUsed.weaponInfo[activeWeaponIndex].shootReady) ShootTimer();
    }

    private void FixedUpdate()
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].isRecharging) RechargeTimer();
    }

    private void LockUntilNextClick()
    {
        if (clickInput == false || isEnemy)
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
        if (weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer > 0) weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer -= Time.deltaTime;
        else
        {
            weaponUsed.weaponInfo[activeWeaponIndex].shootDelayTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.shootDelay;
            weaponUsed.weaponInfo[activeWeaponIndex].shootReady = true;
        }
    }

    private void RechargeTimer()
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer > 0)
        {
            weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer -= Time.fixedDeltaTime;
        }
        else
        {
            RechargeTime();
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
            weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = false;
        }
    }

    private void ProjectileSpawn(bool isCharged)
    {
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunWeapon) ProjectileCreate(false, isCharged);
        else
        {
            for (int i = 0; i < weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunProjectileNumber; i++)
            {
                if (i == 1) ProjectileCreate(false, isCharged);
                else ProjectileCreate(true, isCharged);
            }
        }
    }

    private void ProjectileCreate(bool isShotgun, bool useAltDamage)
    {
        GameObject projectile = Instantiate(weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (!isShotgun) proj.target = target;
        else proj.target = target + new Vector3(Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread), 0, Random.Range(-weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpread));
        if (!useAltDamage) proj.damage = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileDamage;
        else proj.damage = bulletDamage;
        proj.lifeTime = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileLifetime;
        proj.pierces = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectilePierces;
        proj.hasAoe = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileHasAoe;
        proj.aoe = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileAoe;
        proj.aoeVisible = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileAoeVisibleDuration;
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.hitscanWeapon)
        {
            if (!isShotgun) proj.speed = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileSpeed;
            else proj.speed = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileSpeed - Random.Range(0, weaponUsed.weaponInfo[activeWeaponIndex].weapon.shotgunSpeedDifference);
        }
        else
        {
            proj.isRay = true;
            proj.rayNeedsRange = weaponUsed.weaponInfo[activeWeaponIndex].weapon.hitscanLimitedRange;
            proj.rayRange = weaponUsed.weaponInfo[activeWeaponIndex].weapon.hitscanRange;
            proj.rayDuration = weaponUsed.weaponInfo[activeWeaponIndex].weapon.hitscanVisibleTime;
            proj.rayOrigin = projectileStartPosition.transform.position;
        }
    }

    private void InputCheck()
    {
        if (!isEnemy)
        {
            clickInput = playerRef.playerInputs.LeftClickInput;
            target = new Vector3(playerRef.mousePos.VectorPointToShoot.x, projectileStartPosition.transform.position.y, playerRef.mousePos.VectorPointToShoot.z);
        }
        else
        {
            clickInput = true;
            target = new Vector3(playerRef.gameObject.transform.position.x, projectileStartPosition.transform.position.y, playerRef.gameObject.transform.position.z);
        }
        if (clickInput == true && weaponUsed.weaponInfo[activeWeaponIndex].shootReady && !weaponUsed.weaponInfo[activeWeaponIndex].isRecharging && weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent > 0 && !lockUntilNextClick)
        {
            if (weaponUsed.weaponInfo[activeWeaponIndex].weapon.chargedWeapon && !weaponUsed.weaponInfo[activeWeaponIndex].weapon.automaticWeapon) charging = true;
            else
            {
                ProjectileSpawn(false);
                WeaponAmmoDecrease(1);
                if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.automaticWeapon) lockUntilNextClick = true;
            }
        }
    }

    private void ChargeShot()
    {
        if (charging)
        {
            if (clickInput == true)
            {
                if (weaponUsed.weaponInfo[activeWeaponIndex].chargedTimer > 0)
                {
                    weaponUsed.weaponInfo[activeWeaponIndex].chargedTimer -= Time.deltaTime;
                    weaponUsed.weaponInfo[activeWeaponIndex].chargePercentage = (Mathf.Abs(weaponUsed.weaponInfo[activeWeaponIndex].chargedTimer - weaponUsed.weaponInfo[activeWeaponIndex].weapon.chargeTime) * 100) / weaponUsed.weaponInfo[activeWeaponIndex].weapon.chargeTime;
                    chargeText.text = System.Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].chargePercentage, 2).ToString();
                }
                else
                {
                    weaponUsed.weaponInfo[activeWeaponIndex].chargePercentage = 100f;
                    chargeText.text = System.Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].chargePercentage, 2).ToString();
                }
            }
            else
            {
                float damageFluctuation = weaponUsed.weaponInfo[activeWeaponIndex].weapon.chargedMaxDamage - weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileDamage;
                float damageIncrease = (weaponUsed.weaponInfo[activeWeaponIndex].chargePercentage * damageFluctuation) / 100;
                bulletDamage = weaponUsed.weaponInfo[activeWeaponIndex].weapon.projectileDamage + damageIncrease;
                bulletDamage = Mathf.RoundToInt(bulletDamage);
                ProjectileSpawn(true);
                WeaponAmmoDecrease(1);
                weaponUsed.weaponInfo[activeWeaponIndex].chargedTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.chargeTime;
                chargeText.text = string.Empty;
                charging = false;
            }
        }
    }

    public void WeaponAmmoDecrease(int decreaseAmount)
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent > 0)
        {
            weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent -= decreaseAmount;
            if (weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent <= 0)
            {
                if (weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent < 0) weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent = 0;
                weaponUsed.weaponInfo[activeWeaponIndex].isRecharging = true;
            }
            else weaponUsed.weaponInfo[activeWeaponIndex].shootReady = false;
        }
    }

    private void RechargeTime()
    {
        if (!isEnemy) weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.rechargeDelay;
        else weaponUsed.weaponInfo[activeWeaponIndex].rechargeDelayTimer = Random.Range(weaponUsed.weaponInfo[activeWeaponIndex].weapon.enemyRechargeDelayRangeMin, weaponUsed.weaponInfo[activeWeaponIndex].weapon.enemyRechargeDelayRangeMax);
    }

    private void UpdateText()
    {
        if (!isEnemy)
        {
            ammoText.text = weaponUsed.weaponInfo[activeWeaponIndex].weapon.weaponName + "\nAmmo: " + weaponUsed.weaponInfo[activeWeaponIndex].magazineCurrent + "/" + weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent;
            reloadCanvas.SetActive(weaponUsed.weaponInfo[activeWeaponIndex].isRecharging);
        }
    }

    public void RefullAmmo()
    {
        weaponUsed.weaponInfo[activeWeaponIndex].ammoCurrent = weaponUsed.weaponInfo[activeWeaponIndex].weapon.ammoMax;
    }
}
