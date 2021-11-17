using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody thisProjectileRigidbody;
    public Vector3 target;

    private void FixedUpdate()
    {
        ProjectileMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee")) Destroy(this.gameObject);
    }

    protected virtual void ProjectileMovement()
    {
        Vector3 direction = (target - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
        thisProjectileRigidbody.velocity = this.transform.forward * speed;
    }
}
