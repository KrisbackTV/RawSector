using System;
using Unity.VisualScripting;
using UnityEngine;


public class EquipmentData : IItemSource
{
    private GameActions _inventoryActions;

    private ItemInstance _primaryGun;
    private ItemInstance _secondaryGun;
    private ItemInstance _knife;
    private ItemInstance _grenade;
    private ItemInstance _armor;
    private ItemInstance _backpack;
    

    public EquipmentData(GameActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
        if (_inventoryActions != null)
        {
            _inventoryActions.OnGetItemForEquipSlot += GetItemForEquipSlot;
        }
    }
    
    public void Sourse(DragContextItem dragContextItem, Action<bool> callback)
    {
        _inventoryActions.InvokeOnDropItemToEquipmentSlot(this, dragContextItem._itemInstance);
        callback?.Invoke(true);
    }

    public void Receive(DragContextItem dragContextItem, Action<bool> callback)
    {
        _inventoryActions.InvokeOnDropItemToEquipmentSlot(this, dragContextItem._itemInstance);
        callback?.Invoke(true);
    }

    public void Remove(ItemInstance item, Action<bool> callback)
    {
        if (item == null || item._itemData == null)
        {
            callback?.Invoke(false);
            return;
        }

        _inventoryActions.InvokeOnRemoveItemToEquipmentSlot(this, item._itemData.equipmentType);
        callback?.Invoke(true);
    }

    public void GetItemForEquipSlot(EquipmentType type, Action<ItemInstance> callback)
    {
        callback?.Invoke(GetItemToSlot(type));
    }

    public ItemInstance GetItemToSlot(EquipmentType slotType)
    {
        return slotType switch
        {
            EquipmentType.PrimaryGun => _primaryGun,
            EquipmentType.SecondaryGun => _secondaryGun,
            EquipmentType.Knife => _knife,
            EquipmentType.Grenade => _grenade,
            EquipmentType.Armor => _armor,
            EquipmentType.Backpack => _backpack,
            _ => null
        };
    }


    //Для взаимодействия с данными
    public void SetItem(EquipmentType type, ItemInstance item)
    {
        switch (type)
        {
            case EquipmentType.PrimaryGun:
                _primaryGun = item;
                break;
            case EquipmentType.SecondaryGun:
                _secondaryGun = item;
                break;
            case EquipmentType.Knife:
                _knife = item;
                break;
            case EquipmentType.Grenade:
                _grenade = item;
                break;
            case EquipmentType.Armor:
                _armor = item;
                break;
            case EquipmentType.Backpack:
                _backpack = item;
                break;
        }

        _inventoryActions.InvokeOnUpdateToEquipmentSlot(type, item);
    }

}
