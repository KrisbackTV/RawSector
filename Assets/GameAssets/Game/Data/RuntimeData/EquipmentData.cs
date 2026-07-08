using System;
using Unity.VisualScripting;
using UnityEngine;


public class EquipmentData
{
    private ItemInstance _primaryGun;
    private ItemInstance _secondaryGun;
    private ItemInstance _knife;
    private ItemInstance _grenade;
    private ItemInstance _armor;
    private ItemInstance _backpack;

    public ItemInstance GetItemForType(EquipmentType type)
    {
        return type switch
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

    public void SetItemForType(ItemInstance item, EquipmentType type)
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
    }
}
