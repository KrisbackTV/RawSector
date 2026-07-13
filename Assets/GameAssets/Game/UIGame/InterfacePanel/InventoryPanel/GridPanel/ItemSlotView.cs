using UnityEngine;

public class ItemSlotView : MonoBehaviour
{
    [SerializeField] private ItemView _itemView;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool _IsDragging = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetItemSlot(ItemInstance itemInstance, InventoryData inventoryOwner, int startX, int startY, int itemSizeX, int itemSizeY, float cellSize)
    {
        float posX = startX * cellSize;
        float posY = startY * cellSize;

        float sizeX = itemSizeX * cellSize;
        float sizeY = itemSizeY * cellSize;

        SetPosition(posX, posY);
        SetSize(sizeX, sizeY);

        _itemView.SetView(itemInstance._itemSprite, itemInstance._count);
    }

    private void SetPosition(float x, float y)
    {
        _rectTransform.localPosition = new Vector3(x, -y, 0);
    }

    private void SetSize(float x, float y)
    {
        _rectTransform.sizeDelta = new Vector2(x, y);
    }

    public void SetDrag()
    {
        _IsDragging = !_IsDragging;

        _canvasGroup.alpha = _IsDragging ? 0.5f : 1f;
    }
}
