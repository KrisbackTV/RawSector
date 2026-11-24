using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteractable
{
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

    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent<IEntityContext>(out var context))
        {
            var targetInventory = context.InventoryData;

            if (targetInventory != null)
            {
                targetInventory.Inventory.TryAddItem(_itemInstance);
                Destroy(gameObject);
            }
            else
            {
                GameLog.LogDebug($"инвентарь не найден");
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
