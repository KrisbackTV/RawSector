using UnityEngine;

public interface IEnemyStateStatsProvider
{
    EnemyState GetEnemyState();
    MovementState GetMovementState();

    void SetEnemyState(EnemyState stance);
    void SetMovementState(MovementState state);
}
