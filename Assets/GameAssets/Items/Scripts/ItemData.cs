using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Item,
    Ammo,
    Gun,
    Knife,
    Grenade,
    Armor,
    Backpack
}

public enum EquipmentType
{
    Noon,
    PrimaryGun,
    SecondaryGun,
    Knife,
    Grenade,
    Armor,
    Backpack
}

[System.Flags]
public enum ItemActionType
{
    None = 0,
    Use = 1 << 0,
    Modified = 1 << 1,
    Discard = 1 << 2,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/ItemData")]
public abstract class ItemData : ScriptableObject
{
    [Header("Данные о предмете")]
    public int id = 0;
    public string itemName = "Новый предмет";
    public ItemType type = ItemType.Item;
    public int width = 0, height = 0;
    public bool IsEquipped = false;
    public EquipmentType equipmentType = EquipmentType.Noon;
    public int maxCount = 0;

    [Header("Доступные действия")]
    [SerializeField] private ItemActionType _itemActionType;
    public ItemActionType ItemActionType => _itemActionType;

    [Header("Данные для визуала")]
    public Sprite ItemSprite;
    public GameObject worldPrefab;
    public GameObject equipmentPrefab;
}
