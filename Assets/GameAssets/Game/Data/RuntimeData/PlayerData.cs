using UnityEngine;

public class PlayerData
{
    private AssetsLibrarySO _assetsLibrarySO;
    private GameActions _inventoryActions;

    public InventoryData _playerInventory;
    public EquipmentData _equipmentData;
    public PlayerRuntimeData _playerRuntimeData;

    public PlayerData(AssetsLibrarySO assetsLibrarySO, GameActions inventoryActions, PlayerRuntimeData playerStats)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;

        _playerInventory = new InventoryData(assetsLibrarySO._playerInventory, inventoryActions);
        _equipmentData = new EquipmentData(inventoryActions);
        _playerRuntimeData = playerStats;
    }

}
