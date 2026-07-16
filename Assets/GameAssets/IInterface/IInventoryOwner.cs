using UnityEngine;

public interface IInventoryOwner
{
    PlayerConfigSo GetPlayerData();
    InventorySO GetInventory();
    Transform GetTransform();
    IInputProvider GetInput();
}