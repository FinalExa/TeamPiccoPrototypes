using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PillarsAbility : AbilityProjectile
{
    [SerializeField] private float singlePillarRange;
    [SerializeField] private int timesActivatedInDuration;
    private bool rangeActive;
    [SerializeField] private float rangeVisibleDuration;
    private float rangeTimer;
    private float activationTime;
    private float activationTimer;
    [SerializeField] private int pillarsMaxInScene;
    private PillarsAbility[] pillarsInScene;
    [HideInInspector] public int pillarId;

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    public override void OnDeploy()
    {
        PillarsInScene();
        CalculateTimer();
        DamageArea(this.transform.position);
    }

    public override void AbilityEffectDuration()
    {
        SinglePillarDamageTimer();
        if (rangeActive) RangeShow();
    }

    public override void AbilityEffectAfterDuration()
    {
        DestroyProjectile();
    }

    public override void DestroyProjectile()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void CalculateTimer()
    {
        activationTime = durationTime / timesActivatedInDuration;
        activationTimer = activationTime;
    }

    private void PillarsInScene()
    {
        PillarsCheck();
        if (pillarsInScene.Length > pillarsMaxInScene)
        {
            PillarsCleanup();
        }
    }

    private void PillarsCheck()
    {
        pillarsInScene = FindObjectsOfType<PillarsAbility>();
        pillarId = pillarsInScene.Length - 1;
    }

    private void PillarsCleanup()
    {
        foreach (PillarsAbility pillar in pillarsInScene)
        {
            if (pillar.pillarId == 0) GameObject.Destroy(pillar.gameObject);
            else pillar.pillarId--;
        }
        PillarsCheck();
    }

    private void SinglePillarDamageTimer()
    {
        if (activationTimer > 0) activationTimer -= Time.fixedDeltaTime;
        else
        {
            DamageArea(this.transform.position);
            activationTimer = activationTime;
        }
    }

    private void DamageArea(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, singlePillarRange);
        foreach (Collider hit in hits)
        {
            Health enemy = hit.gameObject.GetComponent<Health>();
            if (enemy != null && !enemy.isPlayer)
            {
                enemy.DecreaseHP(damage);
            }
        }
        RangeVisibleSetup();
    }

    private void RangeVisibleSetup()
    {
        rangeTimer = rangeVisibleDuration;
        range.transform.localScale = new Vector3(singlePillarRange, singlePillarRange, singlePillarRange);
        range.SetActive(true);
        rangeActive = true;
    }

    private void RangeShow()
    {
        if (rangeTimer > 0) rangeTimer -= Time.fixedDeltaTime;
        else
        {
            range.SetActive(false);
            rangeActive = false;
        }
    }

    public void RefreshDuration()
    {
        durationTimer = durationTime;
    }

}
