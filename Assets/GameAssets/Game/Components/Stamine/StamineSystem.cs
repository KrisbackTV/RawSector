using System;
using UnityEngine;
using UnityEngine.UI;

public class StamineSystem : MonoBehaviour, IStaminable
{
    private IEnemyStateStatsProvider _enemyStateStatsProvider;
    private IStamineStatsProvider _staminaData;
    private float _delayTimer;

    public void Initialize(IStamineStatsProvider enemyData, IEnemyStateStatsProvider enemyStateStatsProvider)
    {
        if (enemyData == null || enemyStateStatsProvider == null) return;

        _staminaData = enemyData;
        _enemyStateStatsProvider = enemyStateStatsProvider;
    }

    public void ProcessStamine(float deltaTime, MovementState movementState)
    {
        if( _staminaData == null) return;

        if (movementState == MovementState.Sprint && _staminaData.CurrentStamina <= 0f)
        {
            _enemyStateStatsProvider.SetMovementState(MovementState.Walk);
            movementState = MovementState.Walk;
        }

        bool isSprint = movementState == MovementState.Sprint;

        if (isSprint)
        {
            if(_staminaData.CurrentStamina > 0f) TakeStamine(_staminaData.SprintCost * deltaTime);
        }
        else
        {
            if (_delayTimer > 0)
            {
                _delayTimer -= deltaTime;
                return;
            }
            
            if (_staminaData.CurrentStamina < _staminaData.MaxStamina)
            {
                HealStamine(_staminaData.RegenRate * deltaTime);
            }
        }
    }

    public void TakeStamine(float damage)
    {
        if(_staminaData == null || damage <= 0) return;

        _delayTimer = _staminaData.RegenDelay;

        float value = Mathf.Max(0, _staminaData.CurrentStamina - damage);
        _staminaData.UpdateStamina(value);
    }

    public void HealStamine(float amount)
    {
        if (_staminaData == null || amount <= 0) return;

        float value = Mathf.Min(_staminaData.MaxStamina, _staminaData.CurrentStamina + amount);
        _staminaData.UpdateStamina(value);
    }
}
