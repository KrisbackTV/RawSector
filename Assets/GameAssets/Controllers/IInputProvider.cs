using System;
using UnityEngine;

public interface IInputProvider
{
    Vector3 GetMovement();
    Vector2 GetLook();

    event Action OnPronePressed;
    event Action OnJumpPressed;
    event Action OnInventoryPressed;
    event Action OnInteractPressed;
    event Action<bool> OnCrouchPressed;
    event Action<bool> OnSprintPressed;
    event Action<int> OnSlotPressed;

}