using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement section")]
    public float defaultMovementSpeed;
    [HideInInspector] public float currentMovementSpeed;
    [Header("Dash section")]
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;
}
