using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public class UnitMove : MonoBehaviour
{
    private ISpeedStatsProvider _speedProvider;
    private IJumpStatsProvider _jumpProvider;
    private IEnemyStateStatsProvider _enemyStateStatsProvider;
    private IStamineStatsProvider _stamineStatsProvider;
    private IStaminable _stamineSystem;

    private CharacterController _characterController;

    private bool _isLocked = false;
    [SerializeField] private float _gravityMultiplier = 2f;

    private Vector3 _moveDirection;
    private float _verticalVelocity;

    public void Initialize(ISpeedStatsProvider speedStatsProvider, IJumpStatsProvider jumpStatsProvider, IEnemyStateStatsProvider enemyStateStatsProvider, IStamineStatsProvider stamineStatsProvider)
    {
        _speedProvider = speedStatsProvider;
        _jumpProvider = jumpStatsProvider;
        _enemyStateStatsProvider = enemyStateStatsProvider;
        _stamineStatsProvider = stamineStatsProvider;

    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _stamineSystem = GetComponent<IStaminable>();
    }

    public void SetDirection(Vector3 direction) => _moveDirection = direction;

    private void Update()
    {
        if (_speedProvider == null || _characterController == null) return;

        if (_isLocked)
        {
            _moveDirection = Vector3.zero;
        }

        ApplyMovement(_speedProvider.GetCurrentSpeed());
    }

    private void ApplyMovement(float speed)
    {

        Vector3 worldMove = transform.TransformDirection(_moveDirection);
        Vector3 velocity = worldMove * speed;
        
        if (_characterController.isGrounded)
        {
            _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }

        velocity.y = _verticalVelocity;

        _characterController.Move(velocity * Time.deltaTime);
    }


    public bool CanJump()
    {
        if(_characterController == null || _isLocked) return false;

        if (_stamineSystem != null && _stamineStatsProvider != null)
        {
            return _stamineStatsProvider.CurrentStamina >= _stamineStatsProvider.JumpCost;
        }

        return true;
    }

    public void Jump()
    {
        if (CanJump())
        {
            if (_stamineSystem != null)
            {
                _stamineSystem.TakeStamine(_stamineStatsProvider.JumpCost);
            }
            
            _verticalVelocity = MathF.Sqrt(_jumpProvider.GetJumpForce() * -2f * Physics.gravity.y);
            
            _characterController.Move(Vector3.up * 0.1f);
        }
    }

    public void SetLocked(bool value) => _isLocked = value;
}
