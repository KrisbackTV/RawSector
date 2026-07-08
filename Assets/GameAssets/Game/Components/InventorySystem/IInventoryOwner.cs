using UnityEngine;

public interface IInventoryOwner
{
    PlayerDataSO GetPlayerData();
    InventorySO GetInventory();
    Transform GetTransform();
    IInputProvider GetInput();
}