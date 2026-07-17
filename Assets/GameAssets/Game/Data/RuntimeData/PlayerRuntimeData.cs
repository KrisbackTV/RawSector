using System;
using UnityEngine;

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



    public PlayerEntity _ActivePlayer { get; private set; }

    public Vector3 PlayerPosition => _ActivePlayer != null ? _ActivePlayer.transform.position : Vector3.zero;
    public Vector3 Forward => _ActivePlayer != null ? _ActivePlayer.transform.forward : Vector3.forward;
    public bool IsSpawned => _ActivePlayer != null;


    public void RegisterPlayer(PlayerEntity player)
    {
        _ActivePlayer = player;
        Debug.Log("<color=cyan>[PlayerRuntimeData] Живой игрок успешно зарегистрирован в данных!</color>");
    }

    public void UnregisterPlayer()
    {
        _ActivePlayer = null;
    }


    public EquipmentType ActiveSlot { get; private set; } = EquipmentType.PrimaryGun;
    public event Action<EquipmentType> OnActiveSlotChanged;

    public void ChangeActiveSlot(EquipmentType newSlot)
    {
        ActiveSlot = newSlot;
        OnActiveSlotChanged?.Invoke(ActiveSlot);
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