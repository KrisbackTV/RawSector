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

    public void Sourse(DragContextItem contextItem, Action<bool> callback)
    {
        if (contextItem == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnTryAddToFreeSlotToInventory(this, contextItem._itemInstance, callback);
    }

    public void Receive(DragContextItem contextItem, Action<bool> callback)
    {
        if (contextItem == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnTryPlaceItemToInventory(this, contextItem._itemInstance, contextItem._posInventoryGrid.x, contextItem._posInventoryGrid.y, callback);
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
