using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPlateView : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshPro _itemName;
    [SerializeField] TextMeshPro _itemCount;


    public void UpdateView(ItemInstance item)
    {
        if (item == null) return;

        if(_itemImage != null && item._itemSprite != null)
            _itemImage.sprite = item._itemSprite;
        
        if (_itemName != null && item._itemName != null)
            _itemName.text = item._itemName;

        if (_itemCount != null && item._count > 0)
            _itemCount.text = $"x{item._count}";
    }
}
