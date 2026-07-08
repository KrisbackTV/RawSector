using JetBrains.Annotations;
using UnityEngine;
using VContainer;


[CreateAssetMenu(fileName = "InventoryState", menuName = "Architecture/States/InventoryState")]
public class InventoryState : GameState
{
    private AssetsLibrarySO _assetsLibrarySO;
    private SceneLoader _sceneLoader;
    private InputService _inputService;
    private InventoryActions _inventoryActions;

    [Inject]
    public void Construct(AssetsLibrarySO assetsLibraryOS, SceneLoader sceneLoader, InputService inputService, InventoryActions inventoryActions)
    {
        _assetsLibrarySO = assetsLibraryOS;
        _sceneLoader = sceneLoader;
        _inputService = inputService;
        _inventoryActions = inventoryActions;
    }

    public override void EnterState()
    {
        _assetsLibrarySO.OnEscapePressed += EscapePressed;


        _inputService.SwitchControlMap(GameCoreState.Inventory);
        _assetsLibrarySO.OnOpenWindowForType?.Invoke(WindowType.Inventory);

        Debug.Log("State - Inventory");
    }

    private void EscapePressed()
    {
        _assetsLibrarySO.OnPopStateEvent?.Invoke();
    }

    public override void ExitState()
    {
        _assetsLibrarySO.OnEscapePressed -= EscapePressed;
        _inventoryActions.InvokeOnCancelDrag();
    }
}
