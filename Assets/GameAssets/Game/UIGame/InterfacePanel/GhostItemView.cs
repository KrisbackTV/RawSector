using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GhostItemView : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;
    }

    [SerializeField] private Image _itemImage;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(ItemInstance itemInstance)
    {
        if (itemInstance == null)
        {
            Debug.Log("itemInstance == null");
            return;
        }

        _canvasGroup.alpha = 1;
        transform.localScale = Vector3.one;

        int sizeX = itemInstance._width * _assetsLibrarySO.cellSize;
        int sizeY = itemInstance._height * _assetsLibrarySO.cellSize;
        _rectTransform.sizeDelta = new Vector2(sizeX, sizeY);

        _itemImage.sprite = itemInstance._itemSprite;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
}
