using System;
using UnityEngine;

public interface IStamineStatsProvider
{
    float CurrentStamina { get; }
    float MaxStamina { get; }
    float StaminaRegenRate { get; }
    float StaminaRegenDelay { get; }
    float SprintCost { get; }
    float JumpCost { get; }

    event Action<float, float> OnStaminaChanged;

    void UpdateStamina(float value);
}
