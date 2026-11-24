using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : IInventoryProvider
{
    public InventoryData Inventory { get => this; }

    public List<ItemInstance> _itemList;
    public event Action<List<ItemInstance>> OnChanged;

    public InventoryData()
    {
        _itemList = new List<ItemInstance>();
    }

    public void NotifyChange()
    {
        OnChanged?.Invoke(this._itemList);
    }

    public void TryAddItem(ItemInstance item)
    {
        _itemList.Add(item);
        NotifyChange();
    }
}
