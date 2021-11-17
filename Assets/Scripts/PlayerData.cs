using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement section")]
    [HideInInspector] public float currentMovementSpeed;
    public float defaultMovementSpeed;
    [Header("Melee section")]
    public float meleeDuration;
    public float meleeCooldown;
}
