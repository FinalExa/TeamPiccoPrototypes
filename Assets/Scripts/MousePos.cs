using System.Linq;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    [HideInInspector] public Vector3 mousePositionInSpace;
    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void FixedUpdate()
    {
        MouseRaycast();
    }
    private void MouseRaycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        mousePositionInSpace = hit.point;
    }
}
