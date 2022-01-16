using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectileBody : ProjectileBody
{
    public Vector3 target;
    public bool stopAtTarget;
    public bool usesDuration;
    public bool durationActive;
    public bool afterDuration;
    public bool stopsPlayer;
    public float durationTime;
    public float durationTimer;
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

    private void AbilityEffectDuration()
    {
        print("Ciaoooo");
    }

    private void AbilityEffectAfterDuration()
    {
        print("Addioooo");
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
        if (this.gameObject.CompareTag("Projectile") && collision.gameObject.GetComponent<Health>() && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().DecreaseHP(damage);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("ProjectilePlayer") && other.gameObject.GetComponent<Health>() && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().DecreaseHP(damage);
            DestroyProjectile();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!stopAtTarget && !stopsPlayer) DestroyProjectile();
            else StopMovement();
        }
    }
}
