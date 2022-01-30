using UnityEngine;
using System;

public class ProjectileBody : MonoBehaviour
{
    public Rigidbody thisProjectileRigidbody;
    [HideInInspector] public float damage;
    [HideInInspector] public bool pierces;

    public virtual void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.CompareTag("ProjectilePlayer") || (this.gameObject.CompareTag("AbilityProjectile"))) && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            DestroyProjectile(true);
        }
        if (other.gameObject.CompareTag("Wall")) DestroyProjectile(false);
        if (other.gameObject.CompareTag("AbilityProjectile"))
        {
            other.gameObject.GetComponentInChildren<AbilityProjectileBody>().signatureProjectile = this.gameObject.transform.parent.gameObject;
            other.gameObject.GetComponentInChildren<AbilityProjectileBody>().MainFireInteraction();
        }
    }

    public virtual void DestroyProjectile(bool hitEnemy)
    {
        if (hitEnemy)
        {
            if (!pierces) ProjectileDestruction();
        }
        else ProjectileDestruction();
    }

    private void ProjectileDestruction()
    {
        this.thisProjectileRigidbody.velocity = Vector3.zero;
        Destroy(this.gameObject);
    }
}
