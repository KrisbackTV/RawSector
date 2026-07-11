using UnityEngine;

public class PlayerData
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;

    public InventoryData _playerInventory;
    public EquipmentData _equipmentSO;


    public PlayerData(AssetsLibrarySO assetsLibrarySO, InventoryActions inventoryActions)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;

        _playerInventory = new InventoryData(assetsLibrarySO._playerInventory, inventoryActions);
        _equipmentSO = new EquipmentData(inventoryActions);
    }
}
