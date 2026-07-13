using UnityEngine;

public class PlayerData
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;

    public InventoryData _playerInventory;
    public EquipmentData _equipmentData;
    public PlayerStats _playerStats;


    public PlayerData(AssetsLibrarySO assetsLibrarySO, InventoryActions inventoryActions, PlayerStats playerStats)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;

        _playerInventory = new InventoryData(assetsLibrarySO._playerInventory, inventoryActions);
        _equipmentData = new EquipmentData(inventoryActions);
        _playerStats = playerStats;
    }
}
