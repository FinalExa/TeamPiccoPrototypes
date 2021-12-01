using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField] PlayerReferences playerRef;
    [SerializeField] int playerBatteryExpend;
    [SerializeField] bool autoInput;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileParent;
    [SerializeField] GameObject projectileStartPosition;
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
    private void Awake()
    {
        ProjectileBody.absorb += RechargeAmmo;
        playerRef = FindObjectOfType<PlayerReferences>();
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    private void Start()
    {
        if (!autoInput) shootReady = true;
        shootDelayTimer = shootDelay;
        rechargeDelayTimer = rechargeDelay;
        ammoCurrent = ammoMax;
        magazineCurrent = magazineSize;
    }

    private void Update()
    {
        if (!autoInput) InputCheck();
        if (Input.GetKeyDown(KeyCode.R) && magazineCurrent != magazineSize) isRecharging = true;
        UpdateText();
    }

    private void FixedUpdate()
    {
        if (!shootReady) ShootTimer();
        if (isRecharging) RechargeTimer();
    }

    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    private void ShootTimer()
    {
        if (shootDelayTimer == shootDelay && autoInput) AiProjectile();
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
        if (rechargeDelayTimer > 0) rechargeDelayTimer -= Time.fixedDeltaTime;
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
            if (!autoInput) isRecharging = false;
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
        if (playerRef.playerInputs.RightClickInput == true && shootReady && !isRecharging && magazineCurrent > 0)
        {
            if (playerRef.playerController.battery > 0)
            {
                scalingLevel = playerRef.playerController.battery;
                playerRef.playerController.BatteryUpdate(-scalingLevel);
            }
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
}
