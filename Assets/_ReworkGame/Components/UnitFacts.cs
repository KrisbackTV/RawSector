using UnityEngine;

public class UnitFacts
{
    public bool _isGrounded = false;

    public bool _isProneToggled = false;
    public bool _isCrouchToggled = false;

    // --- Ввод движения (пишет UnitMove) ---
    public Vector2 MoveInput; // Направление стика/WASD
    public bool IsSprintRequested = false; // Зажат ли Shift

    // --- Стамина (пишет UnitStamina) ---
    // Пока у тебя нет системы стамины, они будут true по умолчанию
    public bool HasStaminaForSprint = true;
    public bool HasStaminaForJump = true;

    // --- Общие блокировки (смерть, стан, катсцена) ---
    public bool IsLocked = false;

    //CurrentInteractItem
    public IInteractable CurrentInteractable = null;
}
