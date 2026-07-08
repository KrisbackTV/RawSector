using System.Runtime.CompilerServices;
using UnityEngine;
using VContainer;

public class PlayerStats : ISpeedStatsProvider, IJumpStatsProvider, IEnemyStateStatsProvider, IHeightStatsProvider
{
    //DATA:
    private AssetsLibrarySO _assetsLibrarySO;
    private PlayerDataSO _playerDataSO;

    public PlayerStats(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;

        _playerDataSO = assetsLibrarySO._playerDataSO;

        _enemyState = _playerDataSO.enemyStance;
        _movementState = _playerDataSO.movementState;
    }

    //STATS
    public EnemyState _enemyState;
    public MovementState _movementState;



    public float GetSpeed()
    {
        return (_enemyState, _movementState) switch
        {
            (EnemyState.Stand, MovementState.Sprint) => _playerDataSO.sprintSpeed,
            (EnemyState.Stand, MovementState.Walk) => _playerDataSO.standSpeed,
            (EnemyState.Stand, MovementState.Idle) => _playerDataSO.standSpeed,

            (EnemyState.Prone, MovementState.Sprint) => _playerDataSO.proneSpeed,
            (EnemyState.Prone, MovementState.Walk) => _playerDataSO.proneSpeed,
            (EnemyState.Prone, MovementState.Idle) => _playerDataSO.proneSpeed,

            (EnemyState.Crouch, MovementState.Sprint) => _playerDataSO.crouchSpeed,
            (EnemyState.Crouch, MovementState.Walk) => _playerDataSO.crouchSpeed,
            (EnemyState.Crouch, MovementState.Idle) => _playerDataSO.crouchSpeed,

            (EnemyState.InAir, MovementState.Sprint) => _playerDataSO.sprintSpeed,
            (EnemyState.InAir, MovementState.Walk) => _playerDataSO.standSpeed,
            (EnemyState.InAir, MovementState.Idle) => _playerDataSO.standSpeed,

            _ => _playerDataSO.standSpeed
        };
    }

    public float GetHeight()
    {
        return _enemyState switch
        {
            EnemyState.Stand => _playerDataSO.standHeight,
            EnemyState.Crouch => _playerDataSO.crouchHeight,
            EnemyState.Prone => _playerDataSO.proneHeight,
            _ => _playerDataSO.standHeight
        };
    }


    //ИНТЕРФЕЙСЫ:
    public float GetCurrentSpeed() => GetSpeed();
    public float GetSprintSpeed() => _playerDataSO.sprintSpeed;
    public float GetStandSpeed() => _playerDataSO.standSpeed;
    public float GetCrouchSpeed() => _playerDataSO.crouchSpeed;
    public float GetProneSpeed() => _playerDataSO.proneSpeed;

    public float GetCurrentHeight() => GetHeight();
    public float GetStandHeight() => _playerDataSO.standHeight;
    public float GetCrouchHeight() => _playerDataSO.crouchHeight;
    public float GetProneHeight() => _playerDataSO.proneHeight;

    public float GetJumpForce() => _playerDataSO.jumpForse;

    public EnemyState GetEnemyState() => _enemyState;
    public MovementState GetMovementState() => _movementState;
    public EnemyState SetEnemyState(EnemyState stance) => _enemyState = stance;
    public MovementState SetMovementState(MovementState state) => _movementState = state;
}