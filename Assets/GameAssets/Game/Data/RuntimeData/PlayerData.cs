using UnityEngine;

public class PlayerData
{
    private AssetsLibrarySO _assetsLibrarySO;

    public InventoryData _playerInventory;
    public EquipmentData _equipmentSO;


    public PlayerData(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;

        _playerInventory = new InventoryData(assetsLibrarySO._playerInventory);
        _equipmentSO = new EquipmentData();
    }
}
