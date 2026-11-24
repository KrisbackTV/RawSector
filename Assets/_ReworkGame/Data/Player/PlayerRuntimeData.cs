using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerRuntimeData : ISpeedStatsProvider, IJumpStatsProvider, IMovementConfig,
    IHeightStatsProvider, IStamineStatsProvider, IHealthStatsProvider
{
    //Кэш
    private PlayerConfigSo _playerCfg;

    public PlayerRuntimeData(PlayerConfigSo playerConfig)
    {
        if (playerConfig == null)
        {
            GameLog.LogError($"Config остутствует (AssetsLibrary:{playerConfig == null})");
            return;
        }

        _playerCfg = playerConfig;

        CurrentStamina = _playerCfg._staminaStats._maxStamina;
        CurrentHealth = _playerCfg._healthStats._maxHealth;

        GameLog.LogInfo("Статы игрока успешно инициализированны");
    }



    public float SprintSpeed => _playerCfg._movementStats._sprintSpeed;
    public float StandSpeed => _playerCfg._movementStats._standSpeed;
    public float CrouchSpeed => _playerCfg._movementStats._crouchSpeed;
    public float ProneSpeed => _playerCfg._movementStats._proneSpeed;


    public float StandHeight => _playerCfg._stanceStats._standHeight;
    public float CrouchHeight => _playerCfg._stanceStats._crouchHeight;
    public float ProneHeight => _playerCfg._stanceStats._proneHeight;




    public float JumpForce => _playerCfg._movementStats._jumpForce;




    //Stamine
    public float CurrentStamina { get; private set; }
    public float MaxStamina { get => _playerCfg._staminaStats._maxStamina; }
    public float StaminaRegenRate {  get => _playerCfg._staminaStats._staminaRegenRate; }
    public float StaminaRegenDelay { get => _playerCfg._staminaStats._staminaRegenDelay; }
    public float SprintCost { get => _playerCfg._staminaStats._sprintCost; }
    public float JumpCost { get => _playerCfg._staminaStats._jumpCost; }
    
    public event Action<float, float> OnStaminaChanged;
    
    public void UpdateStamina(float value)
    {
        CurrentStamina = Mathf.Clamp(value, 0f, MaxStamina);

        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);
    }




    //Health
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get => _playerCfg._healthStats._maxHealth; }
    public float HealthRegenRate { get => _playerCfg._healthStats._healthRegenRate; }
    public float HealthRegenDelay { get => _playerCfg._healthStats._healthRegenDelay; }
    public event Action<float, float> OnHealthChanged;

    public void UpdateHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(value, 0f, MaxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }



    public float GetSpeed(EnemyState stance, MovementState state)
    {
        return (stance, state) switch
        {
            // Спринт возможен только стоя
            (EnemyState.Stand, MovementState.Sprint) => _playerCfg._movementStats._sprintSpeed,
            (EnemyState.Stand, MovementState.Walk) => _playerCfg._movementStats._standSpeed,
            (EnemyState.Stand, MovementState.Idle) => _playerCfg._movementStats._standSpeed,

            // Присед и лежание — всегда одна скорость, независимо от того, идешь ты или бежишь (хотя бегать лежа нельзя)
            (EnemyState.Crouch, _) => _playerCfg._movementStats._crouchSpeed,
            (EnemyState.Prone, _) => _playerCfg._movementStats._proneSpeed,

            // В воздухе скорость обычно равна базовой скорости стоя
            _ => _playerCfg._movementStats._standSpeed
        };
    }


    public float GetHeight(EnemyState stance)
    {
        return stance switch
        {
            EnemyState.Stand => _playerCfg._stanceStats._standHeight,
            EnemyState.Crouch => _playerCfg._stanceStats._crouchHeight,
            EnemyState.Prone => _playerCfg._stanceStats._proneHeight,
            _ => _playerCfg._stanceStats._standHeight // Для InAir
        };
    }
}