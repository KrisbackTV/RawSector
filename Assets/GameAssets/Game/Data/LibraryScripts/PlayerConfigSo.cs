using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Architecture/Data/PlayerConfigSO")]
public class PlayerConfigSo : ScriptableObject
{
    public StanceStats _stanceStats;
    public MovementStats _movementStats;
    public HealthStats _healthStats;
    public StaminaStats _staminaStats;

    private InventorySO inventorySO;

}
