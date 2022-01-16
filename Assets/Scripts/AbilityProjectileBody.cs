using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectileBody : ProjectileBody
{
    [HideInInspector] public Vector3 target;
    [HideInInspector] public bool stopAtTarget;
    [HideInInspector] public bool usesDuration;
    [HideInInspector] public bool durationActive;
    [HideInInspector] public bool afterDuration;
    [HideInInspector] public bool stopsPlayer;
    [HideInInspector] public bool interactsWithMainShots;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float durationTimer;
    private PlayerReferences playerRefs;

    private void Awake()
    {
        playerRefs = FindObjectOfType<PlayerReferences>();
    }

    private void Update()
    {
        if (stopAtTarget) StopAtTarget();
        else if (usesDuration) durationActive = true;
    }

    private void FixedUpdate()
    {
        if (durationActive) Duration();
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

    private void Duration()
    {
        if (durationTimer > 0)
        {
            durationTimer -= Time.fixedDeltaTime;
            if (!afterDuration)
            {
                AbilityEffectDuration();
                StopPlayer();
            }
        }
        else
        {
            if (afterDuration) AbilityEffectAfterDuration();
            RestartPlayer();
            durationActive = false;
        }
    }

    public virtual void AbilityEffectDuration()
    {
        print("Ciaoooo");
    }

    public virtual void AbilityEffectAfterDuration()
    {
        print("Addioooo");
    }

    public virtual void MainFireInteraction()
    {
        print("Helo");
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
        float distance = Vector3.Distance(this.gameObject.transform.position, target);
        if (distance <= 0.5f)
        {
            StopMovement();
            if (usesDuration) durationActive = true;
        }
    }

    private void StopMovement()
    {
        thisProjectileRigidbody.velocity = Vector3.zero;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget && !stopsPlayer) DestroyProjectile();
            else StopMovement();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget && !stopsPlayer) DestroyProjectile();
            else StopMovement();
        }
    }
}
