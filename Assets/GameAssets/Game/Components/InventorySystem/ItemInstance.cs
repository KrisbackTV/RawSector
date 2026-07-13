using UnityEngine;


public class ItemInstance
{
    public ItemData _itemData {  get; private set; }
    public int _width => _itemData.width;
    public int _height => _itemData.height;

    public string _itemName => _itemData.name;
    public Sprite _itemSprite => _itemData.ItemSprite;


    public int _count;
    
    public ItemInstance(ItemData item, int count = 1)
    {
        _itemData = item;
        _count = count;
    }
}
