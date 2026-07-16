using System;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class ItemWorld : MonoBehaviour, IInteractable
{
    private GameActions _inventoryActions;
    [Inject]
    public void Construct(GameActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
    }

    public ItemData _itemData;
    public ItemInstance _itemInstance;

    [Header("Manual Setup (Scene Only)")]
    [SerializeField] private int _countOverride = 1;


    private void Awake()
    {
        if (_itemInstance == null || _itemInstance._itemData == null)
        {
            if (_itemData != null)
            {
                _itemInstance = new ItemInstance(_itemData, _countOverride);
            }
        }
    }

    public void Initialize(ItemInstance itemInstance)
    {
        _itemInstance = itemInstance;
        _itemData = itemInstance._itemData;
    }

    public void Interact(GameObject initiator)
    {
        if (initiator.TryGetComponent<IPlayerInventoryProvider>(out var inventoryProvider))
        {
            var targetInventory = inventoryProvider.PlayerInventory;
            
            if (targetInventory != null)
            {
                _inventoryActions.InvokeOnTryAddToFreeSlotToInventory(targetInventory, _itemInstance, (IsSuccess) =>
                {
                    if (IsSuccess)
                    {
                        Destroy(gameObject);
                    }
                });

            }
        }
        else
        {
            Debug.Log("не найдено");
        }
    }

    public string GetInteractText()
    {
        if (_itemData != null)
        {
            return ($"{_itemData.name}");
        }
        return ("null");
    }

}
