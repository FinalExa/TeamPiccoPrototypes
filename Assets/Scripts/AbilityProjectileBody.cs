using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectileBody : ProjectileBody
{
    [HideInInspector] public Vector3 target;
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
    [HideInInspector] public float durationTime;
    [HideInInspector] public float durationTimer;
    [HideInInspector] public GameObject originPoint;
    private PlayerReferences playerRefs;

    private void Awake()
    {
        playerRefs = FindObjectOfType<PlayerReferences>();
    }

    public virtual void Start()
    {
        if (stopAfterTime)
        {
            stopTimeCheck = true;
            stopTimer = stopTime;
        }
    }

    private void Update()
    {
        if (stopTimeCheck) StopAfterTime();
        else if (stopAtTarget) StopAtTarget();
        else if (usesDuration) durationActive = true;
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
        }
        else
        {
            AbilityEffectBeforeReachingTarget();
        }
    }

    private void StopMovement()
    {
        thisProjectileRigidbody.velocity = Vector3.zero;
        if (stopTimeCheck) stopTimeCheck = false;
    }

    public override void OnCollisionEnter(Collision collision)
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
                StopMovement();
                if (usesDuration) durationActive = true;
            }
        }
    }
}
