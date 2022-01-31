using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAbility : MonoBehaviour
{
    public SphereCollider myCollider;
    public float ColliderAoe,ColliderAoePlus;
    public Weapon MyAoe;
    void Start()
    {
        MyAoe.projectileAoe = ColliderAoe;
        myCollider.GetComponent<SphereCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePlayer"))
        {
            MyAoe.projectileAoe = ColliderAoePlus;
        }
    }
}
