using System;
using UnityEngine;

public class UnitJump : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IInputProvider _inputProvider;
    private IJumpStatsProvider _jumpProvider;
    private IStamineStatsProvider _stamineStatsProvider;
    private IMotorProvider _motorProvider;
    private IStaminable _stamineSystem;


    [SerializeField] private float _gravityMultiplier = 2f;



    public void OnEntityInitializer(IEntityContext enemyContext)
    {
        if (enemyContext == null)
        {
            GameLog.LogError($"Какой то из обязательных компонентов отсутствует");
            return;
        }
        _unitFacts = enemyContext._unitFacts;

        _inputProvider = enemyContext.InputProvider;

        _jumpProvider = enemyContext.JumpStatsProvider;

        _stamineStatsProvider = enemyContext.StamineStatsProvider;

        _motorProvider = enemyContext.MotorProvider;

        //if (enemyContext.StaminaSystem != null)
        //{
        //    _stamineSystem = enemyContext.StaminaSystem;
        //}


        Subscribe();
    }

    private void Update()
    {
        if (_motorProvider == null || _unitFacts == null) return;

        if (!_unitFacts._isGrounded)
        {
            _motorProvider.VerticalVelocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }
    }


    public bool CanJump()
    {
        if (_motorProvider == null || _unitFacts.IsLocked || !_unitFacts._isGrounded || _unitFacts._isProneToggled)
            return false;

        //if(_enemyStateStatsProvider.EnemyState == EnemyState.Prone)
        //    return false;

        if (_stamineSystem != null)
        {
            if (_stamineStatsProvider == null) return false;
            return _stamineStatsProvider.CurrentStamina >= _stamineStatsProvider.JumpCost;
        }

        return true;
    }

    public void Jump()
    {
        if (!CanJump()) return;
        
        if (_stamineSystem != null)
        {
            _stamineSystem.TakeStamine(_stamineStatsProvider.JumpCost);
        }

        _motorProvider.VerticalVelocity = MathF.Sqrt(_jumpProvider.JumpForce * -2f * Physics.gravity.y * _gravityMultiplier);

        GameLog.LogInfo($"Player for Jump");
    }

    private void Subscribe()
    {
        _inputProvider.OnJumpPressed += Jump;
    }

    private void UnSubscribe()
    {
        _inputProvider.OnJumpPressed -= Jump;
    }

    private void OnDestroy() => UnSubscribe();
}
