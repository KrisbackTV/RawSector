using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class InventoryService : IStartable
{
    private InventoryActions _inventoryActions;

    public InventoryService(InventoryActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
    }

    public void Start()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnGetItemForPosition += GetItemForPosition;
            _inventoryActions.OnTryAddToFreeSlot += TryAddItemToFreeSlot;
            _inventoryActions.OnTryPlaceItem += OnTryPlaceItem;
            _inventoryActions.OnRemoveItem += RemoveItem;
        }
    }


    private void GetItemForPosition(InventoryData inventory, Vector2Int pos, Action<ItemInstance> callback)
    {
        if (inventory == null || pos.x < 0 || pos.x >= inventory.Width || pos.y < 0 || pos.y >= inventory.Height)
        {
            callback?.Invoke(null);
            return;
        }

        callback?.Invoke(inventory._items[pos.x, pos.y]);
    }


    public ItemInstance FindItemForId(InventoryData inventory, int itemId)
    {
        if (inventory == null || itemId == 0)
        {
            Debug.Log("Предмет с таким id не найдет или же id равен 0");
            return null;
        }

        foreach (var item in inventory._itemPosition.Keys)
        {
            if (item._itemData.id == itemId)
            {
                return item;
            }
        }
        return null;
    }

    public void TryAddItemToFreeSlot(InventoryData inventory, ItemInstance item, Action <bool> callback)
    {
        if (inventory == null || item == null)
        {
            callback?.Invoke(false);
            return;
        }

        var existingItems = new List<ItemInstance>(inventory._itemPosition.Keys);
        foreach (var existingItem in existingItems)
        {
            if (existingItem._itemData.id == item._itemData.id)
            {
                Vector2Int pos = inventory._itemPosition[existingItem];

                if (existingItem._count < existingItem._itemData.maxCount)
                {
                    TryStackItem(item, inventory, pos.x, pos.y);

                    if (item._count <= 0)
                    {
                        inventory.NotifyChange();
                        callback?.Invoke(true);
                        return;
                    }
                }
            }
        }

        for (int y = 0; y < inventory.Height; y++)
        {
            for (int x = 0; x < inventory.Width; x++)
            {
                if(TryPlaceItem(inventory, item, x, y))
                {
                    callback?.Invoke(true);
                    return;
                }
            }
        }
        callback?.Invoke(false);
    }


    private void OnTryPlaceItem(InventoryData inventory, ItemInstance item, int x, int y, Action<bool> callback)
    {
        callback?.Invoke(TryPlaceItem(inventory, item, x, y));
    }

    private bool TryPlaceItem(InventoryData inventory, ItemInstance item, int startX, int startY)
    {
        if (!CanPlaceItem(inventory, item, startX, startY))
        {
            ItemInstance iteminstance = inventory._items[startX, startY];

            if (iteminstance != null && iteminstance._itemData.id == item._itemData.id)
            {
                TryStackItem(item, inventory, startX, startY);
                return item._count <= 0;
            }

            return false;
        }

        if (inventory._itemPosition.ContainsKey(item))
        {
            RemoveItem(inventory, item, null);
        }

        inventory._itemPosition[item] = new Vector2Int(startX, startY);

        for (int x = startX; x < startX + item._width; x++)
        {
            for (int y = startY; y < startY + item._height; y++)
            {
                inventory._items[x, y] = item;
            }
        }
        inventory.NotifyChange();
        return true;
    }

    public void RemoveItem(InventoryData inventory, ItemInstance item, Action<bool> callback)
    {
        if (inventory == null || item == null || !inventory._itemPosition.TryGetValue(item, out Vector2Int startPos))
        {
            callback?.Invoke(false);
            return;
        }

        for (int x = startPos.x; x < startPos.x + item._width; x++)
        {
            for (int y = startPos.y; y < startPos.y + item._height; y++)
            {
                if (inventory._items[x, y] == item)
                {
                    inventory._items[x, y] = null;
                }
            }
        }
        inventory._itemPosition.Remove(item);

        inventory.NotifyChange();
        callback?.Invoke(true);
    }

    public bool TryStackItem(ItemInstance Item, InventoryData Inventory, int targetItemX, int targetItemY)
    {
        ItemInstance targetItem = Inventory._items[targetItemX, targetItemY];

        if (targetItem == null || targetItem._itemData.id != Item._itemData.id) return false;

        int canAdd = targetItem._itemData.maxCount - targetItem._count;

        if (canAdd > 0 && Item._count > 0)
        {
            int amountToAdd = Mathf.Min(canAdd, Item._count);
            targetItem._count += amountToAdd;
            Item._count -= amountToAdd;

            Inventory.NotifyChange();
            return true;
        }
        return false;
    }

    public bool CanPlaceItem(InventoryData inventory, ItemInstance item, int startX, int startY)
    {
        if(startX < 0 || startY < 0 || startX + item._width > inventory.Width || startY + item._height > inventory.Height)
            return false;

        for (int x = startX; x < startX + item._width; x++)
        {
            for (int y = startY; y < startY + item._height; y++)
            {
                if (inventory._items[x, y] != null && inventory._items[x, y] != item)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
