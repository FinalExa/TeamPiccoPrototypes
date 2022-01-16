using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    [SerializeField] protected ProjectileBody[] thisProjectileChildren;
    protected float lifeTimer;
    public Vector3 target;
    public float damage;
    public bool isRay;
    public bool rayNeedsRange;
    public float rayRange;
    public float rayDuration;
    public Vector3 rayOrigin;
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
        Vector3 direction = (target - this.transform.position).normalized;
        for (int i = 0; i < thisProjectileChildren.Length; i++) thisProjectileChildren[i].gameObject.SetActive(false);
        Ray ray = new Ray(rayOrigin, direction);
        RaycastHit hit;
        //Vector3 endposition = rayOrigin + (rayRange * direction);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider);
        }
        Debug.DrawRay(rayOrigin, direction, Color.red, rayDuration, true);
    }
}

