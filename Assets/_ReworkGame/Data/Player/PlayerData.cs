using UnityEngine;

public class PlayerData
{
    public InventoryData _playerInventory;
    public EquipmentData _equipmentData;
    public PlayerRuntimeData _playerRuntimeData;

    public PlayerData(PlayerRuntimeData playerStats)
    {
        if (playerStats == null)
        {
            string isPlayerStats = playerStats != null ? "присутствует" : "отсутствует";

            GameLog.LogError($"Какой то из компонентов остутствует (PlayerStats:{isPlayerStats})");
        }

        _playerRuntimeData = playerStats;
        _playerInventory = new InventoryData();

        //_equipmentData = new EquipmentData(_gameActions);

        GameLog.LogInfo("Данные игрока (PlayerData) успешно созданны");
    }

}
