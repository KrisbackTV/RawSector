using System;
using UnityEngine;

public class PlayerController : IInputProvider
{
    public event Action OnPronePressed;
    public event Action OnJumpPressed;
    public event Action OnInventoryPressed;
    public event Action OnInteractPressed;
    public event Action<bool> OnCrouchPressed;
    public event Action<bool> OnSprintPressed;
    public event Action<int> OnSlotPressed;
    public event Action OnDropItemPressed;

    public Vector2 GetLook()
    {
        return Vector2.zero;
    }

    public Vector3 GetMovement()
    {
        return Vector3.zero;
    }
}
