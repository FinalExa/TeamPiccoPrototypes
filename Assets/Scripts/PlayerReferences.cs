using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public PlayerData playerData;
    [HideInInspector] public PlayerInputs playerInputs;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public MousePos mousePos;
    [HideInInspector] public Rigidbody playerRb;
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public Shoot shoot;
    [HideInInspector] public Rotation rotation;
    [HideInInspector] public WeaponCycle weaponCycle;


    private void Awake()
    {
        playerData.currentMovementSpeed = playerData.defaultMovementSpeed;
        playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        playerController = this.gameObject.GetComponent<PlayerController>();
        mousePos = this.gameObject.GetComponent<MousePos>();
        playerRb = this.gameObject.GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        shoot = this.gameObject.GetComponent<Shoot>();
        rotation = this.gameObject.GetComponent<Rotation>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
    }
}
