using UnityEngine;

public class DragContextItem
{
    public ItemInstance _itemInstance { get; }
    public IItemSource _inventoryOwner { get; }

    public Vector2Int _posInventoryGrid { get; set; } = new Vector2Int(-1, -1);
    public EquipmentType _equipmentType { get; set; }

    public DragContextItem(ItemInstance itemInstance, IItemSource inventoryOwner)
    {
        _itemInstance = itemInstance;
        _inventoryOwner = inventoryOwner;
    }
}
