using UnityEngine;

public enum EnemyState
{
    Stand,
    Crouch,
    Prone,
    InAir
}

public enum MovementState
{
    Idle,
    Walk,
    Sprint,
}

public class EnemyDataSO : ScriptableObject
{
    [Header("CurrentState")]
    public EnemyState enemyStance = EnemyState.Stand;
    public MovementState movementState = MovementState.Idle;

    [Header("StanceSettinng")]
    public float standHeight = 1.9f;
    public float crouchHeight = 1.5f;
    public float proneHeight = 0.9f;
    [Space]
    public float sprintSpeed = 8f;
    public float standSpeed = 5f;
    public float crouchSpeed = 3f;
    public float proneSpeed = 1.5f;

    public float jumpForse = 2f;

    public float maxHealth = 100f;

    public float maxStamine = 100f;
    public float regenRateStamine = 0.5f;
    public float regenDelayStamine = 3f;
    public float sprintStaminaCost = 10f;
}

