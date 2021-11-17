using UnityEngine;

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

    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    private void Start()
    {
        shootDelayTimer = shootDelay;
    }

    private void Update()
    {
        InputCheck();
    }

    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    void ShootTimer()
    {
        if (shootDelayTimer == shootDelay)
        {
            if (autoInput) AiProjectile();
            else PlayerProjectile();
        }
        if (shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
        else shootDelayTimer = shootDelay;
    }

    private void AiProjectile()
    {
        GameObject proj = Instantiate(projectile, ProjectilePositionCalculate(), Quaternion.identity, projectileParent.transform);
        proj.GetComponent<Projectile>().target = playerRef.gameObject.transform.position;
    }

    private void PlayerProjectile()
    {
        GameObject proj = Instantiate(projectile, ProjectilePositionCalculate(), Quaternion.identity, projectileParent.transform);
        proj.GetComponent<Projectile>().target = playerRef.mousePos.mousePositionInSpace;
    }

    void InputCheck()
    {
        if (autoInput == false)
        {
            WithPlayerInput();
        }
        else
        {
            ShootTimer();
        }
    }

    void WithPlayerInput()
    {
        if (playerRef.playerInputs.RightClickInput == true && playerRef.playerController.battery >= playerBatteryExpend)
        {
            playerRef.playerController.BatteryUpdate(-playerBatteryExpend);
            ShootTimer();
        }
    }
}
