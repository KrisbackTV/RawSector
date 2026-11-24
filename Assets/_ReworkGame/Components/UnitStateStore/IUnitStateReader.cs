using UnityEngine;

public interface IUnitStateReader
{
    EnemyState UnitEnemyState { get; }
    MovementState UnitMovementState { get; }
}
