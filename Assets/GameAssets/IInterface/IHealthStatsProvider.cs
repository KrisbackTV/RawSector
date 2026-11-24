using System;
using UnityEngine;

public interface IHealthStatsProvider
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    float HealthRegenRate { get; }
    float HealthRegenDelay { get; }

    event Action<float, float> OnHealthChanged;

    void UpdateHealth(float value);
}
