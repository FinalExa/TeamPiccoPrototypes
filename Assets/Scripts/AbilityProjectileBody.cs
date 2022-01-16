using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectileBody : ProjectileBody
{
    public Vector3 target;
    public bool stopAtTarget;

    private void Update()
    {
        if (stopAtTarget) StopAtTarget();
    }

    private void StopAtTarget()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, target);
        if (distance <= 0.4f)
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        thisProjectileRigidbody.velocity = Vector3.zero;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget) DestroyProjectile();
            else StopMovement();
        }
        if (this.gameObject.CompareTag("Projectile") && collision.gameObject.GetComponent<Health>() && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().DecreaseHP(damage);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("ProjectilePlayer") && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget) DestroyProjectile();
            else StopMovement();
        }
    }
}
