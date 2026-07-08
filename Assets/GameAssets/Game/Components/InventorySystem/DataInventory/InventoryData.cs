using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public ItemInstance[,] _items;
    public Dictionary<ItemInstance, Vector2Int> _itemPosition;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public ItemType InventoryType { get; private set; }

    public event Action<InventoryData> OnChanged;

    public InventoryData(InventorySO inventorySO)
    {
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
}
