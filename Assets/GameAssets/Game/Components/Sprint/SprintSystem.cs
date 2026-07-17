using UnityEngine;
using UnityEngine.Windows;
using VContainer;

public class SprintSystem : MonoBehaviour
{
    private IEnemyStateStatsProvider _enemyStateStatsProvider;
    private ISpeedStatsProvider _speedStatsProvider;

    //private StamineSystem stamineSystem;
    //private IInputProvider input;

    bool IsSprintPressed = false;
    bool IsMoving = false;

    public void Initialize(IEnemyStateStatsProvider enemyStateStatsProvider, ISpeedStatsProvider speedStatsProvider)
    {
        _enemyStateStatsProvider = enemyStateStatsProvider;
        _speedStatsProvider = speedStatsProvider;
    }

    public void SetSprint(bool isSprinting)
    {
        IsSprintPressed = isSprinting;
    }

    public void SetMovement(Vector3 direction)
    {
        IsMoving = direction.sqrMagnitude > 0.01f;
    }


    private void Update()
    {
        if (_enemyStateStatsProvider == null) return;

        bool canSprinting = _enemyStateStatsProvider.GetEnemyState() == EnemyState.Stand;
        //bool hasStamina = stamineSystem.CurrentStamine > 0;

        if (IsSprintPressed && canSprinting && IsMoving)
        {
            _enemyStateStatsProvider.SetMovementState(MovementState.Sprint);

            //stamineSystem.SetRegenerate(false);
            //stamineSystem.TakeStamine(enemyDataSO.sprintStaminaCost * Time.deltaTime);
        }
        else if (IsMoving)
        {
            _enemyStateStatsProvider.SetMovementState(MovementState.Walk);
        }
        else
        {
            _enemyStateStatsProvider.SetMovementState(MovementState.Idle);
            //stamineSystem.SetRegenerate(true);
        }
        
    }
}
