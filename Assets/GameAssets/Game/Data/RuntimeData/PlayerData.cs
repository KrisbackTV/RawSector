using UnityEngine;

public class PlayerData
{
    private AssetsLibrarySO _assetsLibrarySO;
    private GameActions _inventoryActions;

    public InventoryData _playerInventory;
    public EquipmentData _equipmentData;
    public PlayerRuntimeData _playerStats;

    public PlayerEntity _ActivePlayer {  get; private set; }

    public Vector3 PlayerPosition => _ActivePlayer != null ? _ActivePlayer.transform.position : Vector3.zero;
    public Vector3 Forward => _ActivePlayer != null ? _ActivePlayer.transform.forward : Vector3.forward;
    public bool IsSpawned => _ActivePlayer != null;


    public PlayerData(AssetsLibrarySO assetsLibrarySO, GameActions inventoryActions, PlayerRuntimeData playerStats)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;

        _playerInventory = new InventoryData(assetsLibrarySO._playerInventory, inventoryActions);
        _equipmentData = new EquipmentData(inventoryActions);
        _playerStats = playerStats;
    }

    public void RegisterPlayer(PlayerEntity player)
    {
        _ActivePlayer = player;
        Debug.Log("<color=cyan>[PlayerRuntimeData] Живой игрок успешно зарегистрирован в данных!</color>");
    }

    public void UnregisterPlayer()
    {
        _ActivePlayer = null;
    }
}
