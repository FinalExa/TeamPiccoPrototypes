using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public bool rotationEnabled;
    private MousePos mousePos;
    private Transform playerCharacterTransform;
    void Awake()
    {
        mousePos = FindObjectOfType<MousePos>();
        playerCharacterTransform = this.gameObject.transform;
    }
    private void Start()
    {
        rotationEnabled = true;
    }
    void Update()
    {
        RotatePlayerToMousePosition();
    }
    private float CalculateAngle(Vector3 player, Vector3 mouse)
    {
        return Mathf.Atan2(mouse.x - player.x, mouse.z - player.z) * Mathf.Rad2Deg;
    }

    public void RotateObjectToLaunch(Transform objectToLaunch, Vector3 endPosition)
    {
        objectToLaunch.localRotation = Quaternion.identity;
        float angle = CalculateAngle(objectToLaunch.position, endPosition);
        playerCharacterTransform.rotation = Quaternion.Euler(new Vector3(objectToLaunch.rotation.x, angle, playerCharacterTransform.rotation.z));
    }
    public void RotatePlayerToMousePosition()
    {
        float angle = CalculateAngle(playerCharacterTransform.position, mousePos.mousePositionInSpace);
        playerCharacterTransform.rotation = Quaternion.Euler(new Vector3(playerCharacterTransform.rotation.x, angle, playerCharacterTransform.rotation.z));
    }
}
