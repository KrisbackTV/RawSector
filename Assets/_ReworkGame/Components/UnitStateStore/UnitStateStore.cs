using UnityEngine;


public class UnitStateStore : IUnitStateReader, IUnitStateWriter
{
    public EnemyState UnitEnemyState { get; private set; } = EnemyState.Stand;
    public MovementState UnitMovementState { get; private set; } = MovementState.Idle;

    public void SetEnemyState(EnemyState stance) => UnitEnemyState = stance;
    public void SetMovementState(MovementState state) => UnitMovementState = state;
}