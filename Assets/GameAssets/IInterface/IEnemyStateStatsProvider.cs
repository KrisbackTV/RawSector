using UnityEngine;

public interface IEnemyStateStatsProvider
{
    EnemyState GetEnemyState();
    MovementState GetMovementState();

    EnemyState SetEnemyState(EnemyState stance);
    MovementState SetMovementState(MovementState state);
}
