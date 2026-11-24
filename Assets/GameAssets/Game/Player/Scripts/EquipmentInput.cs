using UnityEngine;

public class EquipmentInput : MonoBehaviour, IEntityModule
{
    private IEquipmentDataProvider _equipmentDataProvider;
    private IInputProvider _inputProvider;

    public void OnEntityInitializer(IEntityContext context)
    {
        _equipmentDataProvider = context.EquipmentDataProvider;
        _inputProvider = context.InputProvider;

        Subscribe();
    }


    private void HandleSlot(int slotIndex)
    {
        EquipmentType type = slotIndex switch
        {
            1 => EquipmentType.PrimaryGun,
            2 => EquipmentType.SecondaryGun,
            3 => EquipmentType.Knife,
            4 => EquipmentType.Grenade,
            _ => EquipmentType.PrimaryGun,
        };

        _equipmentDataProvider.ChangeActiveSlot(type);
    }

    private void Subscribe()
    {
        if (_inputProvider == null) return;

        _inputProvider.OnSlotPressed += HandleSlot;
    }

    private void UnSubscribe()
    {
        if (_inputProvider == null) return;

        _inputProvider.OnSlotPressed -= HandleSlot;
    }


    private void OnDisable() => UnSubscribe();

    private void OnDestroy() => UnSubscribe();
}

