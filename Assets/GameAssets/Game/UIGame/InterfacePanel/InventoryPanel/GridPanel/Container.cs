using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class Container : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;
    }

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void AddViewItem(ItemInstance itemData, InventoryData inventoryOwner, int posX, int posY)
    {
        if (itemData == null || inventoryOwner == null) return;

        int startX = posX;
        int startY = posY;

        int itemWidth = itemData._width;
        int itemHeight = itemData._height;

        var item = Instantiate(_assetsLibrarySO.UIGridItemSlotPrefab, _rectTransform);

        ItemSlotView itemSlotView = item.GetComponent<ItemSlotView>();

        if (itemSlotView != null)
        {
            itemSlotView.SetItemSlot(itemData, inventoryOwner, startX, startY, itemWidth, itemHeight, _assetsLibrarySO.cellSize);
        }
    }

    public void CleanConteiner()
    {
        foreach (Transform child in _rectTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
