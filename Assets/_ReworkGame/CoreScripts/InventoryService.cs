using Unity.VisualScripting;
using UnityEngine;

public class InventoryService
{
    private InputService _inputService;

    private InventoryData _inventoryData = null;


    public InventoryService(InputService input, InventoryData playerInventory)
    {
        _inputService = input;
        _inventoryData = playerInventory;

        if (_inputService != null)
        {
            _inputService.OnOpenInventory += RequestOpenPlayerInventory;
            _inputService.OnCanceledWindowPressed += RequestCloseInventory;
        }

        EventBus.OnContainerOpened += RequestOpenContainer;
    }

    public void RequestOpenPlayerInventory()
    {
        var context = new InventoryUIContext( _inventoryData, null);

        // Отправляем событие в шину
        EventBus.OpenInventoryUI(context);
    }


    public void RequestOpenContainer(InventoryData chestInventory)
    {
        var context = new InventoryUIContext (_inventoryData, chestInventory);

        EventBus.OpenInventoryUI(context);
    }

    public void RequestCloseInventory()
    {
        EventBus.CloseInventoryUI();
    }
}
