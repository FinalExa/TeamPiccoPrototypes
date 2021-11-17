using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public static Action<int> absorb;
    [SerializeField] private int rechargeValue;
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
            absorb(rechargeValue);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
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
