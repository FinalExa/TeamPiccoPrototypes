using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : DamageTaken
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag(hostileTag) || other.gameObject.CompareTag("Melee")) && !other.gameObject.CompareTag("Player"))
        {
            thisRB.velocity = Vector3.zero;
            if(!other.gameObject.CompareTag("Melee")) Destroy(other.gameObject);
            if (!isDamaged) TakeDamage();
        }

    }
}
