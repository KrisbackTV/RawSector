using System;
using UnityEngine;


public class InputService : IInputProvider
{
    private GameInput inputActions;
    public float Sensivity { get; set; } = 4;

    //Player
    public event Action OnJumpPressed;
    public event Action OnPronePressed;
    public event Action OnInteractPressed;
    public event Action<bool> OnCrouchPressed;
    public event Action<bool> OnSprintPressed;
    public event Action<int> OnSlotPressed;

    //Inventory
    public event Action OnOpenInventory;
    public event Action OnCanceledWindowPressed;
    public event Action OnDropItem;
    public event Action LeftMouseForInventoryPressed;
    public event Action RightMouseForInventoryPressed;


    public void Initialize()
    {
        inputActions = new GameInput();

        inputActions.Player.Disable();
        inputActions.Inventory.Disable();
        inputActions.Menu.Disable();
        inputActions.EscMenu.Disable();
        inputActions.Dialogue.Disable();
        inputActions.Dead.Disable();
        inputActions.Player.Enable();

        inputActions.Player.JumpPressed.performed += _ => OnJumpPressed?.Invoke();
        inputActions.Player.PronePressed.performed += _ => OnPronePressed?.Invoke();
        inputActions.Player.InventoryPressed.performed += _ => OnOpenInventory?.Invoke();
        inputActions.Player.InteractPressed.performed += _ => OnInteractPressed?.Invoke();
        inputActions.Player.EscMenu.performed += _ => OnCanceledWindowPressed?.Invoke();

        inputActions.Player.Sprint.started += _ => OnSprintPressed?.Invoke(true);
        inputActions.Player.Sprint.canceled += _ => OnSprintPressed?.Invoke(false);

        inputActions.Player.Crouch.started += _ => OnCrouchPressed?.Invoke(true);
        inputActions.Player.Crouch.canceled += _ => OnCrouchPressed?.Invoke(false);

        inputActions.Player.Hotbar.performed += ctx =>
        {
            int slot = (int)ctx.ReadValue<float>();
            OnSlotPressed?.Invoke(slot);
        };

        inputActions.Inventory.Drop.performed += _ => OnDropItem?.Invoke();

        inputActions.Inventory.LeftMouse.performed += _ => LeftMouseForInventoryPressed?.Invoke();
        inputActions.Inventory.RightMouse.performed += _ => RightMouseForInventoryPressed?.Invoke();


        inputActions.Player.Enable();
    }

    public Vector3 GetMovement 
    {
        get
        {
            Vector2 move = inputActions.Player.Movement.ReadValue<Vector2>();
            return new Vector3(move.x, 0, move.y);
        }
    }

    public Vector2 GetLook => inputActions.Player.Look.ReadValue<Vector2>();

    public void SwitchControlMap(GameMachineState mapName)
    {
        inputActions.Player.Disable();
        inputActions.Inventory.Disable();
        inputActions.Menu.Disable();
        inputActions.EscMenu.Disable();
        inputActions.Dialogue.Disable();
        inputActions.Dead.Disable();

        switch (mapName)
        {
            case GameMachineState.Menu: 
                inputActions.Menu.Enable(); 
                break;
            case GameMachineState.SaveLoadMenu: 
                inputActions.Menu.Enable(); 
                break;
            case GameMachineState.Gameplay: 
                inputActions.Player.Enable(); 
                break;
            case GameMachineState.Inventory: 
                inputActions.Inventory.Enable(); 
                break;
            case GameMachineState.EscMenu: 
                inputActions.EscMenu.Enable(); 
                break;
            case GameMachineState.Dialogue: 
                inputActions.Dialogue.Enable(); 
                break;
            case GameMachineState.Dead: 
                inputActions.Dead.Enable(); 
                break;
        }
    }

    public void Dispose()
    {
        inputActions?.Dispose();
    }
}
