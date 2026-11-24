using System;

public interface IEquipmentDataProvider
{
    ItemInstance ActiveHandItem { get; }
    event Action<ItemInstance> OnActiveItemChanged;

    void ChangeActiveSlot(EquipmentType newSlot);
}
