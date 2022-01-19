using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SecondaryAbility", menuName = "ScriptableObjects/SecondaryAbility", order = 3)]
public class SecondaryAbility : ScriptableObject
{
    public string secondaryFireName;
    public GameObject secondaryProjectile;
    public float secondaryDamage;
    public float secondaryProjectileSpeed;
    [Tooltip("Toggle if you want the projectile shot to stop at the mouse position instead of going forward that point")]
    public bool secondaryStopsAtMousePosition;
    [Tooltip("Toggle if you want to be casted directly on the point clicked without travelling, works only if it's casted at the mouse position and not otherwise")]
    public bool secondaryFlies;
    [Tooltip("Toggle if you want the projectile shot to last a certain amount of time in the field before disappearing")]
    public bool secondaryDurationToggle;
    public float secondaryDurationTime;
    [Tooltip("Toggle if you want the player to stop moving and shooting during the duration, works only if duration is active and effect after duration is NOT active, also the projectile stopping on the mouse pos must NOT be active")]
    public bool playerIsStoppedDuringSecondaryDuration;
    [Tooltip("Toggle if you want a cooldown after the secondary fire is shot")]
    public bool secondaryCooldownToggle;
    public float secondaryCooldownTime;
    public float secondaryLifetime;
    public int secondaryAmmoCost;
    [Tooltip("Toggle if you want ALL normal weapon shots to interact with the projectile in the field")]
    public bool normalShotsInteractWithSecondaryProjectile;
}
