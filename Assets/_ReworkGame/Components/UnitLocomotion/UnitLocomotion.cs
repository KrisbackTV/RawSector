using UnityEngine;

public class UnitLocomotion : MonoBehaviour, IEntityModule
{
    private UnitFacts _facts;
    private IUnitStateWriter _writer;

    public void OnEntityInitializer(IEntityContext ctx)
    {
        _facts = ctx._unitFacts;
        _writer = ctx._unitStateWriter;
    }

    private void Update()
    {
        // Логика движения
        if (_facts.MoveInput.sqrMagnitude < 0.01f) // Игрок не жмет WASD
        {
            _writer.SetMovementState(MovementState.Idle);
        }
        else if (_facts.IsSprintRequested && _facts.HasStaminaForSprint)
        {
            _writer.SetMovementState(MovementState.Sprint);
        }
        else
        {
            _writer.SetMovementState(MovementState.Walk);
        }
    }
}