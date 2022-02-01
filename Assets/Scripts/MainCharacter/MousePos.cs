using System.Linq;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    private Camera mainCamera;
    public GameObject PointToShoot;

    public Vector3 VectorPointToShoot;

    public Vector3 reticlePosition;
    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }

    public Vector3 reticleNormal;
    public Vector3 ReticleNormal
    {
        get { return reticleNormal; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ReticlePosition, 1f);
    }

    public void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void Update()
    {
        if (mainCamera)
        {
            MouseRaycast();
            VectorPointToShoot = new Vector3(PointToShoot.transform.position.x, PointToShoot.transform.position.y, PointToShoot.transform.position.z);
        }

    }
    private void MouseRaycast()
    {
        Ray screenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
        }
    }
}
