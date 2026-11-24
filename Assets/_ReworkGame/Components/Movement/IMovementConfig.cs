public interface IMovementConfig
{
    float GetSpeed(EnemyState stance, MovementState movement);
    float GetHeight(EnemyState stance);
}