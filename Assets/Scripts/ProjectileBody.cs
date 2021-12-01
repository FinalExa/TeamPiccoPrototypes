using UnityEngine;
using System;

public class ProjectileBody : MonoBehaviour
{
    public static Action<int> absorb;
    [SerializeField] private int rechargeValue;
    public Rigidbody thisProjectileRigidbody;
    public float damage;
    public int scalingLevel;
    public Scaling scaling;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            this.thisProjectileRigidbody.velocity = Vector3.zero;
            absorb(rechargeValue);
            Destroy(this.gameObject);
        }
        if (this.gameObject.CompareTag("ProjectilePlayer") && other.gameObject.GetComponent<Health>())
        {
            if (scalingLevel == 0) other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            else other.gameObject.GetComponent<Health>().DecreaseHP(DamageScalingFormula());
        }
    }

    private float DamageScalingFormula()
    {
        float addedDmg = (damage / 100f) * scaling.scalingPer[scalingLevel - 1];
        float totalDmg = damage + addedDmg;
        print(totalDmg);
        return totalDmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            this.thisProjectileRigidbody.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }
}
