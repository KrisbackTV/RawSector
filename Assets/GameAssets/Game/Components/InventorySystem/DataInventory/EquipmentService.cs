using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using VContainer;

public class EquipmentService
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _actionList;
    private PlayerData _playerData;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InventoryActions uIActionList, PlayerData playerData)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _actionList = uIActionList;
        _playerData = playerData;

        if (_actionList != null)
        {
            _actionList.OnDropItemToSlot += SetItemForSlot;
        }
    }

    public void SetItemForSlot(EquipmentType slotType, ItemInstance item)
    {
        if (_playerData == null)
        {
            Debug.Log("_playerData отсутствует");
            return;
        }

        _playerData._equipmentSO.SetItemForType(item, slotType);
        _actionList.InvokeOnUpdateToSlot(slotType, item);
    }

    public ItemInstance GetItemToSlot(EquipmentType slotType)
    {
        return _playerData._equipmentSO.GetItemForType(slotType);
    }

}
