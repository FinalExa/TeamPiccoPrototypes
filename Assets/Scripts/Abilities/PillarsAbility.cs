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
    [SerializeField] private float rangeBetweenPillarsToActivateAbility;
    [SerializeField] private PillarWall pillarWall;

    public override void AbilityEffectBeforeReachingTarget()
    {
        base.AbilityEffectBeforeReachingTarget();
    }

    public override void OnDeploy()
    {
        PillarsInScene();
        PillarWalls();
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
        List<Health> healths = new List<Health>();
        Collider[] hits = Physics.OverlapSphere(position, singlePillarRange);
        foreach (Collider hit in hits)
        {
            Health enemy = hit.gameObject.GetComponent<Health>();
            if (enemy != null && !enemy.isPlayer && !healths.Contains(enemy))
            {
                enemy.DecreaseHP(damage);
                healths.Add(enemy);
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

    private void PillarWalls()
    {
        foreach (PillarsAbility pillar in pillarsInScene)
        {
            if (pillarId > 0 && pillar != this)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, pillar.gameObject.transform.position);
                if (distance <= rangeBetweenPillarsToActivateAbility)
                {
                    if (Physics.Raycast(this.transform.position, (pillar.transform.position - this.transform.position).normalized, out RaycastHit hit, distance))
                    {
                        if (hit.collider.gameObject.GetComponent<PillarsAbility>() != null)
                        {
                            SpawnWall(pillar, distance);
                        }
                    }
                }
            }
        }
    }

    private void SpawnWall(PillarsAbility pillar, float distance)
    {
        PillarWall wallScript = Instantiate(pillarWall, this.gameObject.transform);
        wallScript.wallStart = this;
        wallScript.wallEnd = pillar;
        GameObject wall = wallScript.gameObject;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance);
        wall.transform.forward = (pillar.gameObject.transform.position - this.transform.position).normalized;
        wall.transform.position = this.transform.position + ((pillar.gameObject.transform.position - this.transform.position).normalized * distance) / 2;
    }
}
