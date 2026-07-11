using System;
using UnityEngine;

public class InventoryActions
{
    //INVENTORY System

    //Logic Action
    public event Action<InventoryData, ItemInstance, Action<bool>> OnTryAddToFreeSlotToInventory;
    public void InvokeOnTryAddToFreeSlotToInventory(InventoryData data, ItemInstance itemInstance, Action<bool> callback) => OnTryAddToFreeSlotToInventory?.Invoke(data, itemInstance, callback);

    public event Action<InventoryData, ItemInstance, int, int, Action<bool>> OnTryPlaceItemToInventory;
    public void InvokeOnTryPlaceItemToInventory(InventoryData inventoryData, ItemInstance item, int startX, int startY, Action<bool> callback) => OnTryPlaceItemToInventory?.Invoke(inventoryData, item, startX, startY, callback);

    public event Action<InventoryData, ItemInstance, Action<bool>> OnRemoveItemToInventory;
    public void InvokeOnRemoveItemForInventory(InventoryData data, ItemInstance itemInstance, Action<bool> callback) => OnRemoveItemToInventory?.Invoke(data, itemInstance, callback);

    public event Action<InventoryData, Vector2Int, Action<ItemInstance>> OnGetItemForPositionToInventory;
    public void InvokeOnGetItemForPositionToInventory(InventoryData inventory, Vector2Int value, Action<ItemInstance> callback) => OnGetItemForPositionToInventory?.Invoke(inventory, value, callback);


    public event Action<ItemInstance, IItemSource, bool> OnStartDrag;
    public void InvokeOnStartDrag(ItemInstance item, IItemSource inventory, bool isCopyItem) => OnStartDrag?.Invoke(item, inventory, isCopyItem);
    
    public event Action<IItemSource> OnEndDrag;
    public void InvokeOnEndDrag(IItemSource inventory) => OnEndDrag?.Invoke(inventory);

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

    public event Action<EquipmentData, ItemInstance> OnDropItemToEquipmentSlot;
    public void InvokeOnDropItemToEquipmentSlot(EquipmentData inventory, ItemInstance item) => OnDropItemToEquipmentSlot?.Invoke(inventory, item);

    public event Action<EquipmentData, EquipmentType> OnRemoveItemToEquipmentSlot;
    public void InvokeOnRemoveItemToEquipmentSlot(EquipmentData data, EquipmentType type) => OnRemoveItemToEquipmentSlot?.Invoke(data, type);

    public event Action<EquipmentType, ItemInstance> OnUpdateToEquipmentSlot;
    public void InvokeOnUpdateToEquipmentSlot(EquipmentType slotType, ItemInstance item) => OnUpdateToEquipmentSlot?.Invoke(slotType, item);
}
