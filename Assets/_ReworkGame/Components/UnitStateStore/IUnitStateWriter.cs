using UnityEngine;

public interface IUnitStateWriter
{
    void SetEnemyState(EnemyState stance);
    void SetMovementState(MovementState state);
}
