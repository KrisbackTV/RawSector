using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Architecture/Data/InventorySO")]
public class InventorySO : ScriptableObject
{
    public int width = 1;
    public int height = 1;
    public ItemType inventoryType = ItemType.Item;
}
