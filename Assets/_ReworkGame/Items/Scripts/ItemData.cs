using UnityEngine;
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
    Drop = 1 << 2,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/ItemData")]
public class ItemData : ScriptableObject
{

    [Header("Данные о предмете")]
    public int id = 0;
    public string itemName = "Новый предмет";
    public ItemType type = ItemType.Item;
    public int width = 0, height = 0;
    public int maxCount = 0;

    public bool IsEquipped = false;
    public EquipmentType equipmentType = EquipmentType.Noon;


    [Header("Доступные действия")]
    [SerializeField] private ItemActionType _itemActionType;
    public ItemActionType ItemActionType => _itemActionType;


    [Header("Данные для визуала")]
    public Sprite ItemSprite;
    public GameObject worldPrefab;
    public GameObject equipmentPrefab;


    [Header("Параметры Оружия (Заполнять только если тип Gun)")]
    [SerializeField] private bool _isWeapon;
    [SerializeField] private GunStats _gunStats;
    public bool IsWeapon => _isWeapon;
    public GunStats GunStats => _gunStats;


    [Header("Параметры Патронов (Заполнять только если тип Ammo)")]
    [SerializeField] private bool _isAmmo;
    [SerializeField] private AmmoStats _ammoStats;
    public bool IsAmmo => _isAmmo;
    public AmmoStats AmmoStats => _ammoStats;
}
