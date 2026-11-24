using UnityEngine;

public class UnitStateMachine : MonoBehaviour, IEntityModule
{
    private UnitFacts _facts;
    private IUnitStateWriter _writer;

    public void OnEntityInitializer(IEntityContext ctx)
    {
        _facts = ctx._unitFacts;       // Берет шину
        _writer = ctx._unitStateWriter; // Берет сейф (на запись)
    }

    private void Update()
    {
        // Просто читает факты и пишет результат в сейф
        if (!_facts._isGrounded && !_facts._isProneToggled && !_facts._isCrouchToggled)
            _writer.SetEnemyState(EnemyState.InAir);
        else if (_facts._isProneToggled)
            _writer.SetEnemyState(EnemyState.Prone);
        else if (_facts._isCrouchToggled)
            _writer.SetEnemyState(EnemyState.Crouch);
        else
            _writer.SetEnemyState(EnemyState.Stand);
    }
}