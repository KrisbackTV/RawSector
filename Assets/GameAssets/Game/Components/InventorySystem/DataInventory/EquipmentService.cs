using NUnit.Framework.Constraints;
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using VContainer;

public class EquipmentService
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _actionList;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InventoryActions uIActionList, PlayerData playerData)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _actionList = uIActionList;

        if (_actionList != null)
        {
            _actionList.OnDropItemToEquipmentSlot += SetItemForSlot;
            _actionList.OnRemoveItemToEquipmentSlot += RemoveItemToSlot;
        }
    }

    public void SetItemForSlot(EquipmentData data, ItemInstance item)
    {
        if (data == null || item == null)
        {
            Debug.Log("data/itemInstance отсутствует");
            return;
        }

        SetItemForType(data, item, null);
    }

    public void RemoveItemToSlot(EquipmentData data, EquipmentType type)
    {
        if (type == EquipmentType.Noon) return;

        data.SetItem(type, null);
    }

    private void SetItemForType(EquipmentData equipmentData, ItemInstance item, Action<bool> callback)
    {
        if (item == null || !item._itemData.IsEquipped)
        {
            callback?.Invoke(false);
            return;
        }

        if (equipmentData.GetItemToSlot(item._itemData.equipmentType) != null)
        {
            callback?.Invoke(false);
            return;
        }

        equipmentData.SetItem(item._itemData.equipmentType, item);
        callback?.Invoke(true);
    }
}
