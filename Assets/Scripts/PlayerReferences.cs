using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public PlayerData playerData;
    [HideInInspector] public PlayerInputs playerInputs;
    [HideInInspector] public Melee melee;
    [HideInInspector] public Rigidbody playerRb;
    [HideInInspector] public Camera mainCamera;


    private void Awake()
    {
        playerData.currentMovementSpeed = playerData.defaultMovementSpeed;
        playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        playerRb = this.gameObject.GetComponent<Rigidbody>();
        melee = this.gameObject.GetComponent<Melee>();
        mainCamera = FindObjectOfType<Camera>();
    }
}
