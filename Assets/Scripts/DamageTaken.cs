using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    [SerializeField] private GameObject objectToColor;
    [SerializeField] private float feedbackDuration;
    [SerializeField] private Rigidbody thisRB;
    private float feedbackTimer;
    private Renderer thisRenderer;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material damagedMaterial;
    private bool isDamaged;

    private void Awake()
    {
        thisRenderer = objectToColor.GetComponent<Renderer>();
    }
    private void Start()
    {
        feedbackTimer = feedbackDuration;
    }
    private void Update()
    {
        DamageCooldown();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            thisRB.velocity = Vector3.zero;
            Destroy(collision.gameObject);
            if (!isDamaged)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        thisRenderer.material = damagedMaterial;
        isDamaged = true;
    }

    private void DamageCooldown()
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
