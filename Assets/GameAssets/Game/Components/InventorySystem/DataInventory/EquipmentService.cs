using System;

public class EquipmentService
{

    public void SetItemForSlot(EquipmentData data, ItemInstance item)
    {
        if (data == null || item == null)
        {
            GameLog.LogInfo("data/itemInstance отсутствует");
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
