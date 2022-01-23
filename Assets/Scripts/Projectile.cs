using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    [SerializeField] protected LineRenderer line;
    [SerializeField] protected ProjectileBody[] thisProjectileChildren;
    protected float lifeTimer;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float damage;
    [HideInInspector] public bool isRay;
    [HideInInspector] public bool rayNeedsRange;
    [HideInInspector] public float rayRange;
    [HideInInspector] public float rayDuration;
    [HideInInspector] public Vector3 rayOrigin;
    private float rayTimer;
    private bool clearLine;
    public virtual void Start()
    {
        lifeTimer = lifeTime;
        if (!isRay) ProjectileMovement();
        else Hitscan();
    }

    public virtual void Update()
    {
        LifeTime();
        if (isRay) ClearLine();
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
        bool itHit = false;
        for (int i = 0; i < thisProjectileChildren.Length; i++) thisProjectileChildren[i].gameObject.SetActive(false);
        if (Physics.Raycast(rayOrigin, direction, out hit, distance))
        {
            itHit = true;
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
            }
        }
        if (!rayNeedsRange || (rayNeedsRange && itHit)) HitscanDrawLine(rayOrigin, (hit.point).normalized * Vector3.Distance(Vector3.zero, hit.point));
        else
        {
            Vector3 lastLocation = (target - rayOrigin).normalized * distance;
            HitscanDrawLine(rayOrigin, (lastLocation).normalized * Vector3.Distance(Vector3.zero, rayOrigin + lastLocation));
            //Debug.DrawRay(rayOrigin, (target - rayOrigin).normalized * distance, Color.red, rayDuration);
        }
    }

    private void HitscanDrawLine(Vector3 origin, Vector3 destination)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Clear();
        positions.Add(origin);
        positions.Add(destination);
        line.positionCount = 2;
        line.SetPositions(positions.ToArray());
        line.gameObject.SetActive(true);
        rayTimer = rayDuration;
        clearLine = true;
    }

    private void ClearLine()
    {
        if (clearLine)
        {
            if (rayTimer > 0) rayTimer -= Time.deltaTime;
            else
            {
                line.gameObject.SetActive(false);
                rayTimer = rayDuration;
                clearLine = false;
            }
        }
    }
}

