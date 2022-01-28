using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongAbility : AbilityProjectileBody
{

    [SerializeField] private float gongRadius;
    [SerializeField] private GameObject rangeObj;
    [SerializeField] private float feedbackUptime;
    [SerializeField] private float repelDistance;
    private float feedbackUptimer;
    private bool isActive;

    public override void Start()
    {
        base.Start();
        feedbackUptimer = feedbackUptime;
        durationActive = true;
        OnDeploy();
    }

    private void Update()
    {
        if (isActive) FeedbackTimer();
    }

    public override void OnDeploy()
    {
        GongEffect(this.transform.position, gongRadius);
    }

    public override void MainFireInteraction()
    {
        GongEffect(this.transform.position, gongRadius);
        DestroySignature();
    }

    private void GongEffect(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy"))
            {
                GameObject enemy = hits[i].gameObject;
                enemy.GetComponent<Health>().DecreaseHP(damage);
                Vector3 direction = -(this.transform.position - enemy.transform.position).normalized * repelDistance;
                enemy.transform.Translate(direction);
            }
        }
        ActivateRange();
    }

    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }

    private void ActivateRange()
    {
        if (!isActive)
        {
            isActive = true;
            rangeObj.transform.localScale = new Vector3(gongRadius, gongRadius, gongRadius);
            rangeObj.SetActive(true);
        }
    }

    private void DeactivateRange()
    {
        rangeObj.SetActive(false);
        isActive = false;
    }

    private void FeedbackTimer()
    {
        if (feedbackUptimer > 0) feedbackUptimer -= Time.deltaTime;
        else
        {
            feedbackUptimer = feedbackUptime;
            DeactivateRange();
        }
    }
}
