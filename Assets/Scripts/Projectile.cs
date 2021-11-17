using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody thisProjectileRigidbody;
    private float lifeTimer;
    public Vector3 target;

    private void Start()
    {
        lifeTimer = lifeTime;
        ProjectileMovement();
    }

    private void Update()
    {
        LifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            this.thisProjectileRigidbody.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }

    protected virtual void ProjectileMovement()
    {
        Vector3 direction = (target - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
        thisProjectileRigidbody.velocity = this.transform.forward * speed;
    }

    private void LifeTime()
    {
        if (lifeTimer > 0) lifeTimer -= Time.deltaTime;
        else
        {
            Destroy(this.gameObject);
        }
    }
}
