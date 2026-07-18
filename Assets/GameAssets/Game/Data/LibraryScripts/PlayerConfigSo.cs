using System;
using UnityEngine;

public enum EnemyState
{
    Stand,
    Crouch,
    Prone,
    InAir
}

public enum MovementState
{
    Idle,
    Walk,
    Sprint,
}

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Architecture/Data/PlayerConfigSO")]
public class PlayerConfigSo : ScriptableObject
{
    public StanceStats _stanceStats;
    public MovementStats _movementStats;
    public HealthStats _healthStats;
    public StaminaStats _staminaStats;

    private InventorySO inventorySO;

}
