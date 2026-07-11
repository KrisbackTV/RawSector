using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : IItemSource
{
    private InventoryActions _inventoryActions;

    public ItemInstance[,] _items;
    public Dictionary<ItemInstance, Vector2Int> _itemPosition;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public ItemType InventoryType { get; private set; }

    public event Action<InventoryData> OnChanged;

    public InventoryData(InventorySO inventorySO, InventoryActions inventoryActions)
    {
        _inventoryActions = inventoryActions;

        Width = inventorySO.width;
        Height = inventorySO.height;

        InventoryType = inventorySO.inventoryType;

        _items = new ItemInstance[Width, Height];
        _itemPosition = new Dictionary<ItemInstance, Vector2Int>();
    }

    public void NotifyChange()
    {
        OnChanged?.Invoke(this);
    }

    public void Sourse(ItemInstance item, Action<bool> callback)
    {
        if (item == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnTryAddToFreeSlotToInventory(this, item, callback);
    }

    public void Receive(ItemInstance item, Action<bool> callback)
    {
        if (item == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnTryAddToFreeSlotToInventory(this, item, callback);
    }

    public void Remove(ItemInstance item, Action<bool> callback)
    {
        if (item == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnRemoveItemForInventory(this, item, callback);
    }
}
