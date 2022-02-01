using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectile : Projectile
{
    [HideInInspector] public bool stopAfterTime;
    [HideInInspector] public bool stopTimeCheck;
    [HideInInspector] public float stopTime;
    [HideInInspector] public float stopTimer;
    [HideInInspector] public bool stopAtTarget;
    [HideInInspector] public bool usesDuration;
    [HideInInspector] public bool durationActive;
    [HideInInspector] public bool afterDuration;
    [HideInInspector] public bool stopsPlayer;
    [HideInInspector] public bool interactsWithMainShots;
    [HideInInspector] public GameObject signatureProjectile;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float durationTimer;
    [HideInInspector] public GameObject originPoint;
    [HideInInspector] public bool hitSomething;
    private PlayerReferences playerRefs;

    public override void Awake()
    {
        base.Awake();
        playerRefs = FindObjectOfType<PlayerReferences>();
    }

    public override void Update()
    {
        base.Update();
        if (stopTimeCheck) StopAfterTime();
        else if (stopAtTarget) StopAtTarget();
        else if (usesDuration) durationActive = true;
        else if (!usesDuration) OnDeploy();
    }

    public void StopAfterTimeSetup()
    {
        stopAfterTime = true;
    }

    public void StopAtTargetLocation()
    {
        stopAtTarget = true;
    }
    public void SetDurationInfos(bool usesDuration, float durationTimeValue, bool stopPlayer, bool interactionWithMainShots, GameObject originPosition)
    {
        SetDurationTimer(usesDuration, durationTimeValue);
        stopsPlayer = stopPlayer;
        interactsWithMainShots = interactionWithMainShots;
        originPoint = originPosition;
    }

    public override void Start()
    {
        base.Start();
        if (stopAfterTime)
        {
            stopTimeCheck = true;
            stopTimer = stopTime;
        }
    }

    private void FixedUpdate()
    {
        if (durationActive && usesDuration) Duration();
    }

    public void SetDurationTimer(bool ud, float durationTimeValue)
    {
        usesDuration = ud;
        if (usesDuration)
        {
            durationTime = durationTimeValue;
            durationTimer = durationTime;
        }
    }

    private void StopAfterTime()
    {
        if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
            AbilityEffectBeforeReachingTarget();
        }
        else
        {
            OnDeploy();
            if (usesDuration) durationActive = true;
            StopMovement();
        }
    }

    private void Duration()
    {
        if (durationTimer > 0)
        {
            durationTimer -= Time.fixedDeltaTime;
            AbilityEffectDuration();
            StopPlayer();
        }
        else
        {
            AbilityEffectAfterDuration();
            RestartPlayer();
            durationActive = false;
        }
    }

    public virtual void AbilityEffectDuration()
    {
        return;
    }

    public virtual void AbilityEffectAfterDuration()
    {
        return;
    }

    public virtual void MainFireInteraction()
    {
        return;
    }

    public virtual void AbilityEffectBeforeReachingTarget()
    {
        return;
    }

    public virtual void OnDeploy()
    {
        return;
    }

    public virtual void OnEnemyHit()
    {
        return;
    }

    protected void DestroySignature()
    {
        GameObject.Destroy(signatureProjectile);
    }

    private void StopPlayer()
    {
        if (stopsPlayer)
        {
            playerRefs.playerInputs.enabled = false;
            playerRefs.playerInputs.StopAllInputs();
            playerRefs.rotation.enabled = false;
            playerRefs.weaponCycle.enabled = false;
        }
    }

    private void RestartPlayer()
    {
        if (stopsPlayer)
        {
            playerRefs.playerInputs.enabled = true;
            playerRefs.rotation.enabled = true;
            playerRefs.weaponCycle.enabled = true;
        }
    }

    private void StopAtTarget()
    {
        float distance = Vector3.Distance(new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z), new Vector3(target.x, 0f, target.z));
        if (distance <= 1f)
        {
            StopMovement();
            if (usesDuration) durationActive = true;
            OnDeploy();
        }
        else
        {
            AbilityEffectBeforeReachingTarget();
        }
    }

    private void StopMovement()
    {
        projectileRigidbody.velocity = Vector3.zero;
        OnDeploy();
        if (stopTimeCheck) stopTimeCheck = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget && !stopAfterTime && !stopsPlayer) DestroyProjectile();
            else StopMovement();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget && !stopAfterTime && !stopsPlayer) DestroyProjectile();
            else
            {
                hitSomething = true;
                StopMovement();
                if (usesDuration) durationActive = true;
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            hitSomething = true;
            OnEnemyHit();
        }
    }
}
