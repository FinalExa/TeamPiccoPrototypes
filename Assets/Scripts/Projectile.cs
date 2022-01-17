using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    [SerializeField] protected ProjectileBody[] thisProjectileChildren;
    protected float lifeTimer;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float damage;
    [HideInInspector] public bool isRay;
    [HideInInspector] public bool rayNeedsRange;
    [HideInInspector] public float rayRange;
    [HideInInspector] public float rayDuration;
    [HideInInspector] public Vector3 rayOrigin;
    public virtual void Start()
    {
        lifeTimer = lifeTime;
        if (!isRay) ProjectileMovement();
        else Hitscan();
    }

    public virtual void Update()
    {
        LifeTime();
    }

    public virtual void ProjectileMovement()
    {
        Vector3 direction = (target - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
        for (int i = 0; i < thisProjectileChildren.Length; i++)
        {
            thisProjectileChildren[i].thisProjectileRigidbody.velocity = this.transform.forward * speed;
            thisProjectileChildren[i].damage = damage;
        }
    }

    public virtual void LifeTime()
    {
        if (lifeTimer > 0) lifeTimer -= Time.deltaTime;
        else
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Hitscan()
    {
        float distance = Mathf.Infinity;
        if (rayNeedsRange) distance = rayRange;
        Vector3 direction = (target - rayOrigin).normalized;
        RaycastHit hit;
        for (int i = 0; i < thisProjectileChildren.Length; i++) thisProjectileChildren[i].gameObject.SetActive(false);
        if (Physics.Raycast(rayOrigin, direction, out hit, distance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
            Debug.DrawRay(rayOrigin, hit.point, Color.red, rayDuration);
        }
    }
}

