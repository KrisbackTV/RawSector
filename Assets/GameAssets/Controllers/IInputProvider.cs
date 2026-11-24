using System;
using UnityEngine;

public interface IInputProvider
{
    float Sensivity { get; }
    Vector3 GetMovement { get; }
    Vector2 GetLook { get; }

    event Action OnPronePressed;
    event Action OnJumpPressed;
    event Action OnOpenInventory;
    event Action OnInteractPressed;
    event Action<bool> OnCrouchPressed;
    event Action<bool> OnSprintPressed;
    event Action<int> OnSlotPressed;
}