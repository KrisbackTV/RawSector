using System;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class ItemWorld : MonoBehaviour, IInteractable
{
    private InventoryActions _inventoryActions;
    [Inject]
    public void Construct(InventoryActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
    }

    public ItemData _itemdataSO;
    public ItemInstance _itemInstance;

    [Header("Manual Setup (Scene Only)")]
    [SerializeField] private int _countOverride = 1;


    private void Awake()
    {
        if (_itemInstance == null || _itemInstance._itemData == null)
        {
            if (_itemdataSO != null)
            {
                _itemInstance = new ItemInstance(_itemdataSO, _countOverride);
            }
        }
    }

    public void Setup(ItemInstance itemInstance)
    {
        _itemInstance = itemInstance;
        _itemdataSO = itemInstance._itemData;
    }

    public void Interact(GameObject initiator)
    {
        PlayerData playerData = initiator.GetComponentInChildren<PlayerManager>()._playerData;

        if (playerData != null)
        {
            _inventoryActions.InvokeOnTryAddToFreeSlotToInventory( playerData._playerInventory, _itemInstance, (IsSuccess) =>
            {
                if (IsSuccess)
                {
                    Destroy(gameObject);
                }
            });

        }
    }

    public string GetInteractText()
    {
        if (_itemdataSO != null)
        {
            return ($"{_itemdataSO.name}");
        }
        return ("null");
    }

}
