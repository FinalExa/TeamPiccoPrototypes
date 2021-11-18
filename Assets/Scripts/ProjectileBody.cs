using UnityEngine;
using System;

public class ProjectileBody : MonoBehaviour
{
    public static Action<int> absorb;
    [SerializeField] private int rechargeValue;
    public Rigidbody thisProjectileRigidbody;

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
}
