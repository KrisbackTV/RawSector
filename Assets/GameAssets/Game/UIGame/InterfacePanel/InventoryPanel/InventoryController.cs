using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPanelWiew _inventoryPanel;

    private void Start()
    {
        //_gameActions.OnOpenInventory += _inventoryPanel.UpdateInventoryPanel;
        //_playerData._playerInventory.OnChanged += _inventoryPanel.UpdateInventoryPanel;
    }

    private void UnSubscribe()
    {
        //_gameActions.OnOpenInventory -= _inventoryPanel.UpdateInventoryPanel;
        //_playerData._playerInventory.OnChanged -= _inventoryPanel.UpdateInventoryPanel;
    }



    private void OnDestroy() => UnSubscribe();
}
