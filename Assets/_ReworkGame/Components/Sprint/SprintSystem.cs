using UnityEngine;

public class SprintSystem : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IInputProvider _inputProvider;

    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null) return;

        _unitFacts = context._unitFacts;
        _inputProvider = context.InputProvider;

        Subscribe();
    }

    public void SetSprint(bool isSprinting)
    {
        if (_unitFacts == null) return;

        _unitFacts.IsSprintRequested = isSprinting;

        GameLog.LogInfo($"IsSprinting - {isSprinting}");
    }


    private void Subscribe()
    {
        _inputProvider.OnSprintPressed += SetSprint;
    }

    private void UnSubscribe()
    {
        _inputProvider.OnSprintPressed -= SetSprint;
    }


    private void OnDestroy() => UnSubscribe();
}
