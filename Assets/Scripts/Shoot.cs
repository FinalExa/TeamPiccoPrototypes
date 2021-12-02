using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField] PlayerReferences playerRef;
    [SerializeField] int playerBatteryExpend;
    [SerializeField] bool autoInput;
    [SerializeField] bool autoFirstShootStop;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileParent;
    [SerializeField] GameObject projectileStartPosition;
    [SerializeField] GameObject reloadCanvas;
    [SerializeField] private float shootDelay;
    float shootDelayTimer;
    private bool shootReady;
    [SerializeField] private float rechargeDelay;
    float rechargeDelayTimer;
    private bool isRecharging;
    [SerializeField] private int weaponRechargeValue;
    [SerializeField] private int ammoMax;
    private int ammoCurrent;
    [SerializeField] private int magazineSize;
    private int magazineCurrent;
    [SerializeField] private Text ammoText;
    private int scalingLevel;
    [SerializeField] private float ammoRegenTime;
    private float ammoRegenTimer;
    private void Awake()
    {
        ProjectileBody.absorb += RechargeAmmo;
        playerRef = FindObjectOfType<PlayerReferences>();
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    private void Start()
    {
        if (!autoInput)
        {
            reloadCanvas.SetActive(false);
            shootReady = true;
        }
        else
        {
            autoFirstShootStop = true;
        }
        shootDelayTimer = shootDelay;
        rechargeDelayTimer = rechargeDelay;
        ammoRegenTimer = ammoRegenTime;
        ammoCurrent = ammoMax;
        magazineCurrent = magazineSize;
    }

    private void Update()
    {
        if (!autoInput) InputCheck();
        if (!autoInput) UseStack();
        if (Input.GetKeyDown(KeyCode.R) && magazineCurrent != magazineSize) isRecharging = true;
        UpdateText();
    }

    private void FixedUpdate()
    {
        if (!shootReady) ShootTimer();
        if (isRecharging) RechargeTimer();
        if (!autoInput && ammoCurrent < ammoMax) AmmoRegen();
    }

    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    private void AmmoRegen()
    {
        if (ammoRegenTimer > 0) ammoRegenTimer -= Time.fixedDeltaTime;
        else
        {
            ammoRegenTimer = ammoRegenTime;
            ammoCurrent++;
        }
    }

    private void ShootTimer()
    {
        if (shootDelayTimer == shootDelay && autoInput)
        {
            if (autoFirstShootStop) autoFirstShootStop = false;
            else AiProjectile();
        }
        if (shootDelayTimer > 0) shootDelayTimer -= Time.fixedDeltaTime;
        else
        {
            shootDelayTimer = shootDelay;
            if (!autoInput) shootReady = true;
        }
    }

    private void RechargeTimer()
    {
        if (rechargeDelayTimer == rechargeDelay && autoInput) AiProjectile();
        if (rechargeDelayTimer > 0)
        {
            if (!reloadCanvas.activeSelf && !autoInput) reloadCanvas.SetActive(true);
            rechargeDelayTimer -= Time.fixedDeltaTime;
        }
        else
        {
            scalingLevel = 0;
            rechargeDelayTimer = rechargeDelay;
            if (ammoCurrent >= magazineSize)
            {
                if (magazineCurrent == 0)
                {
                    magazineCurrent = magazineSize;
                    ammoCurrent -= magazineSize;
                }
                else
                {
                    int ammoPartial = magazineSize - magazineCurrent;
                    magazineCurrent += ammoPartial;
                    ammoCurrent -= ammoPartial;
                }
            }
            else
            {
                magazineCurrent = ammoCurrent;
                ammoCurrent = 0;
            }
            if (!autoInput)
            {
                isRecharging = false;
                reloadCanvas.SetActive(false);
            }
        }
    }

    private void AiProjectile()
    {
        GameObject proj = Instantiate(projectile, ProjectilePositionCalculate(), Quaternion.identity, projectileParent.transform);
        proj.GetComponent<Projectile>().target = playerRef.gameObject.transform.position;
    }

    private void PlayerProjectile()
    {
        GameObject proj = Instantiate(projectile, ProjectilePositionCalculate(), Quaternion.identity, projectileParent.transform);
        proj.GetComponent<Projectile>().target = playerRef.mousePos.VectorPoitToShoot;
        proj.GetComponent<Projectile>().scalingLevel = scalingLevel;
    }

    private void InputCheck()
    {
        if (playerRef.playerInputs.LeftClickInput == true && shootReady && !isRecharging && magazineCurrent > 0)
        {
            magazineCurrent--;
            PlayerProjectile();
            if (magazineCurrent == 0)
            {
                isRecharging = true;
            }
            else shootReady = false;
        }
    }

    public void RechargeAmmo(int value)
    {
        ammoCurrent = Mathf.Clamp(ammoCurrent + weaponRechargeValue, 0, ammoMax);
    }

    private void UpdateText()
    {
        if (!autoInput) ammoText.text = "Ammo: " + magazineCurrent + "/" + ammoCurrent;
    }

    private void UseStack()
    {
        if (playerRef.playerController.battery > 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            scalingLevel = playerRef.playerController.battery;
            playerRef.playerController.BatteryUpdate(-scalingLevel);
        }
    }
}
