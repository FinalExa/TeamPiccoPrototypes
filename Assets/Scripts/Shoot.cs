using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] PlayerInputs playerInput;
    [SerializeField] bool autoInput;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileParent;
    [SerializeField] GameObject projectileStartPosition;
    [SerializeField] private float shootDelay;
    float shootDelayTimer;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInputs>();
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
            GameObject proj = Instantiate(projectile, ProjectilePositionCalculate(), Quaternion.identity, projectileParent.transform);
            proj.GetComponent<Projectile>().target = playerInput.gameObject.transform.position;
        }
        if (shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
        else shootDelayTimer = shootDelay;
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
        if (playerInput.RightClickInput == true)
        {
            ShootTimer();
        }
    }
}
