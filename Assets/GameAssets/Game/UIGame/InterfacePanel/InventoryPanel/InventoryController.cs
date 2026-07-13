using UnityEngine;
using VContainer;

public class InventoryController : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InputService _inputService;
    private InventoryService _inventoryService;
    private PlayerData _playerData;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InputService inputService, InventoryService inventoryService, PlayerData playerData)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputService = inputService;
        _inventoryService = inventoryService;
        _playerData = playerData;

    }

    [SerializeField] private InventoryPanelWiew _inventoryPanel;

    private void Start()
    {
        //input



        
        _assetsLibrarySO.OnOpenInventoryEvent += _inventoryPanel.UpdateInventoryPanel;
        _playerData._playerInventory.OnChanged += _inventoryPanel.UpdateInventoryPanel;
    }

    private void UnSubscribe()
    {
        //input



        _assetsLibrarySO.OnOpenInventoryEvent -= _inventoryPanel.UpdateInventoryPanel;
        _playerData._playerInventory.OnChanged -= _inventoryPanel.UpdateInventoryPanel;
    }



    private void OnDisable() => UnSubscribe();
    private void OnDestroy() => UnSubscribe();
}
