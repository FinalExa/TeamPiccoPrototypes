using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    [SerializeField] protected string hostileTag;
    [SerializeField] protected GameObject objectToColor;
    [SerializeField] protected float feedbackDuration;
    [SerializeField] protected Rigidbody thisRB;
    protected float feedbackTimer;
    protected Renderer thisRenderer;
    [SerializeField] protected Material baseMaterial;
    [SerializeField] protected Material damagedMaterial;
    protected bool isDamaged;

    protected virtual void Awake()
    {
        thisRenderer = objectToColor.GetComponent<Renderer>();
    }
    protected virtual void Start()
    {
        feedbackTimer = feedbackDuration;
    }
    protected virtual void Update()
    {
        DamageCooldown();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(hostileTag) && !collision.gameObject.CompareTag("Player"))
        {
            thisRB.velocity = Vector3.zero;
            Destroy(collision.gameObject);
            if (!isDamaged) TakeDamage();
        }
    }

    protected virtual void TakeDamage()
    {
        thisRenderer.material = damagedMaterial;
        isDamaged = true;
    }

    protected virtual void DamageCooldown()
    {
        if (isDamaged)
        {
            if (feedbackTimer > 0) feedbackTimer -= Time.deltaTime;
            else
            {
                thisRenderer.material = baseMaterial;
                feedbackTimer = feedbackDuration;
                isDamaged = false;
            }
        }
    }
}
