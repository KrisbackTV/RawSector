using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using VContainer;

public class PlayerRuntimeData : ISpeedStatsProvider, IJumpStatsProvider, IEnemyStateStatsProvider, IHeightStatsProvider
{
    //DATA:
    private AssetsLibrarySO _assetsLibrarySO;
    private PlayerConfigSo _playerDataSO;

    public PlayerRuntimeData(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;

        _playerDataSO = assetsLibrarySO._playerDataSO;
    }

    //STATS
    public EnemyState _enemyState = EnemyState.Stand;
    public MovementState _movementState = MovementState.Idle;


    public float Speed => (_enemyState, _movementState) switch
    {
                (EnemyState.Stand, MovementState.Sprint) => _playerDataSO._movementStats._sprintSpeed,
                (EnemyState.Stand, MovementState.Walk) => _playerDataSO._movementStats._standSpeed,
                (EnemyState.Stand, MovementState.Idle) => _playerDataSO._movementStats._standSpeed,

                (EnemyState.Prone, MovementState.Sprint) => _playerDataSO._movementStats._proneSpeed,
                (EnemyState.Prone, MovementState.Walk) => _playerDataSO._movementStats._proneSpeed,
                (EnemyState.Prone, MovementState.Idle) => _playerDataSO._movementStats._proneSpeed,

                (EnemyState.Crouch, MovementState.Sprint) => _playerDataSO._movementStats._crouchSpeed,
                (EnemyState.Crouch, MovementState.Walk) => _playerDataSO._movementStats._crouchSpeed,
                (EnemyState.Crouch, MovementState.Idle) => _playerDataSO._movementStats._crouchSpeed,

                (EnemyState.InAir, MovementState.Sprint) => _playerDataSO._movementStats._sprintSpeed,
                (EnemyState.InAir, MovementState.Walk) => _playerDataSO._movementStats._standSpeed,
                (EnemyState.InAir, MovementState.Idle) => _playerDataSO._movementStats._standSpeed,

                _ => _playerDataSO._movementStats._standSpeed
    };

    public float Height => (_enemyState) switch
    {
        EnemyState.Stand => _playerDataSO._stanceStats._standHeight,
        EnemyState.Crouch => _playerDataSO._stanceStats._crouchHeight,
        EnemyState.Prone => _playerDataSO._stanceStats._proneHeight,
        _ => _playerDataSO._stanceStats._standHeight
    };

    //ИНТЕРФЕЙСЫ:
    public float GetCurrentSpeed() => Speed;
    public float GetSprintSpeed() => _playerDataSO._movementStats._sprintSpeed;
    public float GetStandSpeed() => _playerDataSO._movementStats._standSpeed;
    public float GetCrouchSpeed() => _playerDataSO._movementStats._crouchSpeed;
    public float GetProneSpeed() => _playerDataSO._movementStats._proneSpeed;

    public float GetCurrentHeight() => Height;
    public float GetStandHeight() => _playerDataSO._stanceStats._standHeight;
    public float GetCrouchHeight() => _playerDataSO._stanceStats._crouchHeight;
    public float GetProneHeight() => _playerDataSO._stanceStats._proneHeight;

    public float GetJumpForce() => _playerDataSO._movementStats._jumpForce;

    public EnemyState GetEnemyState() => _enemyState;
    public MovementState GetMovementState() => _movementState;

    public void SetEnemyState(EnemyState stance) => _enemyState = stance;
    public void SetMovementState(MovementState state) => _movementState = state;
}