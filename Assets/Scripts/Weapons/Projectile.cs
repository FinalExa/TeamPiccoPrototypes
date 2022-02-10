using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float lifeTime;
    [SerializeField] protected GameObject line;
    [SerializeField] protected GameObject range;
    [HideInInspector] public Rigidbody projectileRigidbody;
    protected float lifeTimer;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float damage;
    [HideInInspector] public bool hasAoe;
    [HideInInspector] public float aoe;
    [HideInInspector] public float aoeVisible;
    [HideInInspector] public bool isRay;
    [HideInInspector] public bool rayNeedsRange;
    [HideInInspector] public float rayRange;
    [HideInInspector] public float rayDuration;
    [HideInInspector] public Vector3 rayOrigin;
    [HideInInspector] public bool pierces;
    protected float aoeTimer;
    protected bool aoeDone;
    protected float rayTimer;
    protected bool clearLine;
    protected MeshRenderer meshRenderer;
    protected Collider projectileCollider;
    protected bool objectActive;

    public virtual void Awake()
    {
        projectileRigidbody = this.gameObject.GetComponent<Rigidbody>();
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        projectileCollider = this.gameObject.GetComponent<Collider>();
    }

    public virtual void Start()
    {
        objectActive = true;
        lifeTimer = lifeTime;
        if (!isRay) ProjectileMovement();
        else
        {
            meshRenderer.enabled = false;
            projectileCollider.enabled = false;
            Hitscan();
        }
    }

    public virtual void Update()
    {
        LifeTime();
        if (isRay) ClearLine();
        if (aoeDone) ShowAoe();
    }

    public virtual void DestroyProjectile()
    {
        this.projectileRigidbody.velocity = Vector3.zero;
        meshRenderer.enabled = false;
        projectileCollider.enabled = false;
        objectActive = false;
    }

    private void AoeBehaviour(Vector3 startPos)
    {
        List<EnemyPattern> enemies = new List<EnemyPattern>();
        Collider[] hits = Physics.OverlapSphere(startPos, aoe);
        foreach (Collider hit in hits)
        {
            EnemyPattern pattern = hit.GetComponent<EnemyPattern>();
            if (pattern != null && !enemies.Contains(pattern))
            {
                hit.GetComponent<Health>().DecreaseHP(damage);
                enemies.Add(pattern);
            }
        }
        StartAoeVisible(startPos);
    }

    private void StartAoeVisible(Vector3 startPos)
    {
        aoeTimer = aoeVisible;
        range.transform.localScale = new Vector3(aoe, aoe, aoe);
        range.transform.position = startPos;
        range.SetActive(true);
        aoeDone = true;
    }

    private void ShowAoe()
    {
        if (aoeTimer > 0) aoeTimer -= Time.deltaTime;
        else
        {
            range.SetActive(false);
        }
    }

    public virtual void ProjectileMovement()
    {
        Vector3 direction = (target - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
        projectileRigidbody.velocity = this.transform.forward * speed;
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
        range.SetActive(false);
        if (Physics.Raycast(rayOrigin, direction, out hit, distance))
        {
            itHit = true;
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (!hasAoe) hit.collider.gameObject.GetComponent<Health>().DecreaseHP(damage);
                else AoeBehaviour(hit.point);
            }
            if (hit.collider.gameObject.CompareTag("AbilityProjectile"))
            {
                print("CIAO!");
                if (hit.collider.gameObject.GetComponent<AbilityProjectile>() != null) hit.collider.gameObject.GetComponent<AbilityProjectile>().MainFireInteraction();
                else if (hit.collider.gameObject.GetComponentInParent<AbilityProjectile>() != null) hit.collider.gameObject.GetComponentInParent<AbilityProjectile>().MainFireInteraction();
            }
            if (hit.collider.gameObject.CompareTag("Wall") && hasAoe)
            {
                AoeBehaviour(hit.point);
            }
        }
        if (!rayNeedsRange || (rayNeedsRange && itHit)) HitscanDrawLine(rayOrigin, hit.point);
        else
        {
            Vector3 lastLocation = rayOrigin + ((target - rayOrigin).normalized * distance);
            HitscanDrawLine(rayOrigin, lastLocation);
        }
    }

    private void HitscanDrawLine(Vector3 origin, Vector3 destination)
    {
        float distance = Vector3.Distance(origin, destination);
        line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, distance);
        float angle = Mathf.Atan2(destination.x - origin.x, destination.z - origin.z) * Mathf.Rad2Deg;
        line.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        line.transform.position += (destination - origin).normalized * distance / 2;
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

    public virtual void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("ProjectilePlayer") && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy") && !aoeDone)
        {
            if (!hasAoe) other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            else AoeBehaviour(this.transform.position);
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (hasAoe) AoeBehaviour(this.transform.position);
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("AbilityProjectile") && other.gameObject.GetComponent<AbilityProjectile>() != null && this.CompareTag("ProjectilePlayer"))
        {
            if (hasAoe) AoeBehaviour(this.transform.position);
            other.gameObject.GetComponent<AbilityProjectile>().signatureProjectile = this.gameObject;
            other.gameObject.GetComponent<AbilityProjectile>().MainFireInteraction();
        }
    }
}