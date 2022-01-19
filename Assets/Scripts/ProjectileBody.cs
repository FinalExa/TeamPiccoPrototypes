using UnityEngine;
using System;

public class ProjectileBody : MonoBehaviour
{
    public Rigidbody thisProjectileRigidbody;
    [HideInInspector] public float damage;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("ProjectilePlayer") && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            other.GetComponent<EnemyPattern>().alerted = true;
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Wall")) DestroyProjectile();
        if (other.gameObject.CompareTag("AbilityProjectile")) other.gameObject.GetComponentInChildren<AbilityProjectileBody>().MainFireInteraction();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall")) DestroyProjectile();
        if (this.gameObject.CompareTag("Projectile") && collision.gameObject.GetComponent<Health>() && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().DecreaseHP(damage);
        }
    }

    public virtual void DestroyProjectile()
    {
        this.thisProjectileRigidbody.velocity = Vector3.zero;
        Destroy(this.gameObject);
    }
}
