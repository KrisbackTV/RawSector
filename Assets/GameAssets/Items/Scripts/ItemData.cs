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

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/ItemData")]
public abstract class ItemData : ScriptableObject
{
    public int id = 0;
    public string itemName = "Новый предмет";
    public int width = 0, height = 0;
    public ItemType type = ItemType.Item;
    public bool IsEquipped = false;
    public EquipmentType equipmentType = EquipmentType.Noon;
    public int maxCount = 0;

    public Color color = Color.white;
    public Sprite ItemSprite;
    public GameObject worldPrefab;
    public GameObject equipmentPrefab;
}
