using System;
using Unity.VisualScripting;
using UnityEngine;

public class CrouchAndProne : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IHeightStatsProvider _heightStats;
    private IInputProvider _inputProvider;

    private CharacterController _characterController;

    [Header("Setting")]
    private int _layerMask;

    public void OnEntityInitializer(IEntityContext entityContext)
    {
        if (entityContext == null)
        {
            GameLog.LogError($"Какой то из компонентов отсутствует");
        }

        _unitFacts = entityContext._unitFacts;
        _heightStats = entityContext.HeightStatsProvider;
        _inputProvider = entityContext.InputProvider;

        _characterController = entityContext._characterController;

        _layerMask = ~LayerMask.GetMask("Player");

        Subscribe();
    }

    public void SetCrouchState(bool value)
    {
        if (_unitFacts == null) return;

        _unitFacts._isCrouchToggled = value;

        if (_unitFacts._isCrouchToggled)
        {
            _unitFacts._isProneToggled = false;
        }
        GameLog.LogInfo($"IsCrouch - {_unitFacts._isProneToggled}");
    }

    public void SetProneState()
    {

        if(_unitFacts == null) return;

        if (_unitFacts._isProneToggled && !CanStandUp()) return;

        _unitFacts._isProneToggled = !_unitFacts._isProneToggled;

        if (_unitFacts._isProneToggled)
        {
            _unitFacts._isCrouchToggled = false;
        }
        GameLog.LogInfo($"IsProne - {_unitFacts._isProneToggled}");
    }


    public bool CanStandUp()
    {
        Vector3 rayStart = transform.position+ _characterController.center + Vector3.up * (_characterController.height / 2f - 0.1f);
        float rayLength = (_heightStats.StandHeight - _characterController.height) + 0.1f;

        Debug.DrawRay(rayStart, Vector3.up *  rayLength, Color.red);

        bool isStandUp = Physics.Raycast(rayStart, Vector3.up, rayLength, _layerMask);

        return !isStandUp;
    }


    private void Subscribe()
    {
        _inputProvider.OnCrouchPressed += SetCrouchState;
        _inputProvider.OnPronePressed += SetProneState;
    }

    private void UnSubscribe()
    {
        _inputProvider.OnCrouchPressed -= SetCrouchState;
        _inputProvider.OnPronePressed -= SetProneState;
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}
