using UnityEngine;
using System.Collections.Generic;

public class ProjectileBody : MonoBehaviour
{
    public Rigidbody thisProjectileRigidbody;
    [HideInInspector] public float damage;
    [HideInInspector] public bool hasAoe;
    [HideInInspector] public float aoe;
    [HideInInspector] public float aoeVisible;
    private float aoeTimer;
    private bool aoeDone;
    [SerializeField] private GameObject range;
    [HideInInspector] public bool pierces;


    public virtual void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.CompareTag("ProjectilePlayer") || (this.gameObject.CompareTag("AbilityProjectile"))) && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy") && !aoeDone)
        {
            if (!hasAoe) other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            else AoeBehaviour();
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (hasAoe) AoeBehaviour();
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("AbilityProjectile"))
        {
            if (hasAoe) AoeBehaviour();
            other.gameObject.GetComponentInChildren<AbilityProjectileBody>().signatureProjectile = this.gameObject.transform.parent.gameObject;
            other.gameObject.GetComponentInChildren<AbilityProjectileBody>().MainFireInteraction();
        }
    }

    private void Update()
    {
        if (aoeDone) ShowAoe();
    }

    public virtual void DestroyProjectile()
    {
        this.thisProjectileRigidbody.velocity = Vector3.zero;
        Destroy(this.gameObject);
    }

    private void AoeBehaviour()
    {
        List<EnemyPattern> enemies = new List<EnemyPattern>();
        Collider[] hits = Physics.OverlapSphere(this.transform.position, aoe);
        foreach (Collider hit in hits)
        {
            EnemyPattern pattern = hit.GetComponent<EnemyPattern>();
            if (pattern != null && !enemies.Contains(pattern))
            {
                hit.GetComponent<Health>().DecreaseHP(damage);
                enemies.Add(pattern);
            }
        }
        StartAoeVisible();
    }

    private void StartAoeVisible()
    {
        aoeTimer = aoeVisible;
        range.transform.localScale = new Vector3(aoe, aoe, aoe);
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
}
