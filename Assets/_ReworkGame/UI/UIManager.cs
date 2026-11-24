using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InventoryPanelController _inventoryPanelController;


    public void Initialize()
    {
        if (_inventoryPanelController != null)
            _inventoryPanelController.Initialize();

        Subscribe();
    }

    

    private void Subscribe()
    {
        EventBus.OnInventoryUIOpened += _inventoryPanelController.OpenInventory;
        EventBus.OnCloseVindow += _inventoryPanelController.CloseInventory;
    }
    
    private void UnSubscribe()
    {
        EventBus.OnInventoryUIOpened -= _inventoryPanelController.OpenInventory;
        EventBus.OnCloseVindow -= _inventoryPanelController.CloseInventory;
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}
