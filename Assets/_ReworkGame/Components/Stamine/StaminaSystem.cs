using UnityEngine;

public class StaminaSystem : MonoBehaviour, IStaminable, IEntityModule
{
    private IStamineStatsProvider _staminaData;
    private IUnitStateReader _unitStateReader;
    private UnitFacts _unitFacts;


    private float _delayTimer;


    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null) return;

        _staminaData = context.StamineStatsProvider;
        _unitStateReader = context._unitStateReader;
        _unitFacts = context._unitFacts;
    }


    private void Update()
    {
        if( _staminaData ==  null || _unitStateReader == null || _unitFacts == null) return;

        ProcessStamina(Time.deltaTime, _unitStateReader.UnitMovementState);

        _unitFacts.HasStaminaForSprint = _staminaData.CurrentStamina > 0f;
        _unitFacts.HasStaminaForJump = _staminaData.CurrentStamina >= _staminaData.JumpCost;
    }

    public void ProcessStamina(float deltaTime, MovementState movementState)
    {
        if( _staminaData == null) return;   

        bool isSprint = movementState == MovementState.Sprint;

        if (isSprint)
        {
            if(_staminaData.CurrentStamina > 0f) 
                TakeStamine(_staminaData.SprintCost * deltaTime);
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
                HealStamine(_staminaData.StaminaRegenRate * deltaTime);
            }
        }
    }

    public void TakeStamine(float damage)
    {
        if(_staminaData == null || damage <= 0) return;

        _delayTimer = _staminaData.StaminaRegenDelay;

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
