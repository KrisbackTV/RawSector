using System;
using UnityEngine;

public class InventoryActions
{
    //INVENTORY System

    //Logic Action
    public event Action<InventoryData, ItemInstance, Action<bool>> OnTryAddToFreeSlot;
    public void InvokeOnTryAddToFreeSlot(InventoryData data, ItemInstance itemInstance, Action<bool> callback) => OnTryAddToFreeSlot?.Invoke(data, itemInstance, callback);

    public event Action<InventoryData, ItemInstance, int, int, Action<bool>> OnTryPlaceItem;
    public void InvokeOnTryPlaceItem(InventoryData inventoryData, ItemInstance item, int startX, int startY, Action<bool> callback) => OnTryPlaceItem?.Invoke(inventoryData, item, startX, startY, callback);

    public event Action<InventoryData, ItemInstance, Action<bool>> OnRemoveItem;
    public void InvokeOnRemoveItem(InventoryData data, ItemInstance itemInstance, Action<bool> callback) => OnRemoveItem?.Invoke(data, itemInstance, callback);

    public event Action<InventoryData, Vector2Int, Action<ItemInstance>> OnGetItemForPosition;
    public void InvokeOnGetItemForPosition(InventoryData inventory, Vector2Int value, Action<ItemInstance> callback) => OnGetItemForPosition?.Invoke(inventory, value, callback);


    public event Action<ItemInstance, InventoryData, bool> OnStartDrag;
    public void InvokeOnStartDrag(ItemInstance item, InventoryData inventory, bool isCopyItem) => OnStartDrag?.Invoke(item, inventory, isCopyItem);
    
    public event Action<InventoryData, Vector2Int> OnEndDrag;
    public void InvokeOnEndDrag(InventoryData inventory, Vector2Int targetCell) => OnEndDrag?.Invoke(inventory, targetCell);

    public event Action OnCancelDrag;
    public void InvokeOnCancelDrag() => OnCancelDrag?.Invoke();

    public event Action<Action<bool>> OnIsDragging;
    public void InvokeOnIsDragging(Action<bool> callback) => OnIsDragging?.Invoke(callback);

    public event Action<ItemInstance> OnShowGhostItem;
    public void InvokeOnShowGhostItem(ItemInstance item) => OnShowGhostItem?.Invoke(item);

    public event Action OnHideGhostItem;
    public void InvokeOnHideGhostItem() => OnHideGhostItem?.Invoke();

    public event Action<Action<ItemInstance>> OnGetCurrentDragItem;
    public void InvokeOnGetCurrentDragItem(Action<ItemInstance> callback) => OnGetCurrentDragItem?.Invoke(callback);


    public event Action<ItemInstance> OnDropItemInWorld;
    public void InvokeOnDropItemInWorld(ItemInstance item) => OnDropItemInWorld?.Invoke(item);




    //UI Action












    //EQUIPMENT System

    public event Action<EquipmentType, ItemInstance> OnDropItemToSlot;
    public void InvokeOnDropItemToSlot(EquipmentType slotType, ItemInstance item) => OnDropItemToSlot?.Invoke(slotType, item);

    public event Action<EquipmentType, ItemInstance> OnUpdateToSlot;
    public void InvokeOnUpdateToSlot(EquipmentType slotType, ItemInstance item) => OnUpdateToSlot?.Invoke(slotType, item);
}
