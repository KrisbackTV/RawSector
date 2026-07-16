using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;


public class InputService : IInputProvider, IStartable, IDisposable
{
    private AssetsLibrarySO _assetsLibrarySO;
    private GameActions _inventoryActions;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, GameActions inventoryActions)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
        
    }

    private GameInput inputActions;

    //Player
    public event Action OnJumpPressed;
    public event Action OnPronePressed;
    public event Action OnInventoryPressed;
    public event Action OnInteractPressed;
    public event Action<bool> OnCrouchPressed;
    public event Action<bool> OnSprintPressed;
    public event Action<int> OnSlotPressed;

    //Inventory
    public event Action OnCanceledWindowPressed;
    public event Action OnDropItem;
    public event Action LeftMouseForInventoryPressed;
    public event Action RightMouseForInventoryPressed;


    public void Start()
    {
        inputActions = new GameInput();

        inputActions.Menu.Esc.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();


        inputActions.Player.JumpPressed.performed += _ => OnJumpPressed?.Invoke();
        inputActions.Player.PronePressed.performed += _ => OnPronePressed?.Invoke();
        inputActions.Player.InventoryPressed.performed += _ => OnInventoryPressed?.Invoke();
        inputActions.Player.InteractPressed.performed += _ => OnInteractPressed?.Invoke();

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


        inputActions.Menu.Esc.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();
        inputActions.Player.EscMenu.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();
        inputActions.Inventory.Close.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();
        inputActions.EscMenu.Esc.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();
        inputActions.Dialogue.Esc.performed += _ => _assetsLibrarySO.OnEscapePressed?.Invoke();


        inputActions.Inventory.LeftMouse.performed += _ => LeftMouseForInventoryPressed?.Invoke();
        inputActions.Inventory.RightMouse.performed += _ => RightMouseForInventoryPressed?.Invoke();


        inputActions.Player.Enable();
    }

    public Vector3 GetMovement()
    {
        Vector2 move = inputActions.Player.Movement.ReadValue<Vector2>();
        return new Vector3(move.x, 0, move.y);
    }

    public Vector2 GetLook() => inputActions.Player.Look.ReadValue<Vector2>();

    public void SwitchControlMap(GameCoreState mapName)
    {
        inputActions.Player.Disable();
        inputActions.Inventory.Disable();
        inputActions.Menu.Disable();
        inputActions.EscMenu.Disable();
        inputActions.Dialogue.Disable();

        switch (mapName)
        {
            case GameCoreState.Gameplay: inputActions.Player.Enable(); break;
            case GameCoreState.Inventory: inputActions.Inventory.Enable(); break;
            case GameCoreState.Menu: inputActions.Menu.Enable(); break;
            case GameCoreState.EscMenu: inputActions.EscMenu.Enable(); break;
            case GameCoreState.Dialogue: inputActions.Dialogue.Enable(); break;
        }
    }

    public void Dispose()
    {
        inputActions?.Dispose();
    }
}
