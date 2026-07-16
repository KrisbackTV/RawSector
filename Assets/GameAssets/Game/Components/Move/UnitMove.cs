using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public class UnitMove : MonoBehaviour
{
    private ISpeedStatsProvider _speedProvider;
    private IJumpStatsProvider _jumpProvider;
    private IEnemyStateStatsProvider _enemyStateStatsProvider;

    private CharacterController _characterController;

    private bool _isLocked = false;
    [SerializeField] private float _gravityMultiplier = 2f;

    private Vector3 _moveDirection;
    private float _verticalVelocity;

    [Inject]
    public void Construct(ISpeedStatsProvider speedStatsProvider, IJumpStatsProvider jumpStatsProvider, IEnemyStateStatsProvider enemyStateStatsProvider)
    {
        _speedProvider = speedStatsProvider;
        _jumpProvider = jumpStatsProvider;
        _enemyStateStatsProvider = enemyStateStatsProvider;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void SetDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

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

    public void Jump()
    {
        if (_characterController.isGrounded && !_isLocked)
        {
            _verticalVelocity = MathF.Sqrt(_jumpProvider.GetJumpForce() * -2f * Physics.gravity.y);

            //Костыль, иначе не работает ->
            _characterController.Move(Vector3.up * 0.1f);
        }
    }

    public void SetLocked(bool value) => _isLocked = value;
}
