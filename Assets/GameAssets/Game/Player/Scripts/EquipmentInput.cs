using UnityEngine;
using VContainer;

public class EquipmentInput : MonoBehaviour
{
    private InputService _inputProvider;
    private PlayerData _runtimeData;

    [Inject]
    public void Construct(InputService inputProvider, PlayerData playerData)
    {
        _inputProvider = inputProvider;
        _runtimeData = playerData;
    }

    private void Start()
    {
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

        _runtimeData._playerRuntimeData.ChangeActiveSlot(type);
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


    private void OnDisable()
    {
        UnSubscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}

