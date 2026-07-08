using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using VContainer;

public class EquipSlotView : MonoBehaviour, IPointerDownHandler
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InventoryActions inventoryActions)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;

        if (_inventoryActions != null)
        {
            _inventoryActions.OnUpdateToSlot += UpdateSlotView;
        }
    }

    [SerializeField] private GameObject _slotObj;
    [SerializeField] private RectTransform _slotRTransform;
    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotName;
    [SerializeField] private TextMeshProUGUI _slotCount;

    [SerializeField] private EquipmentType _slotType;

    private void Awake()
    {
        _slotObj = GetComponent<GameObject>();
        _slotRTransform = GetComponent<RectTransform>();

        if (_slotImage == null || _slotName == null || _slotCount == null)
        {
            Debug.Log("отсутствует картинка, название, кол-во  В ИНСПЕКТОРЕ");
        }
    }


    private void UpdateSlotView(EquipmentType type, ItemInstance itemInstance)
    {
        if (type != _slotType) return;

        if (_slotImage != null)
        {
            _slotImage.sprite = itemInstance._itemSprite;
        }

        if (_slotName != null)
        {
            _slotName.text = itemInstance._itemName;
        }

        if (_slotCount != null)
        {
            if (itemInstance._count > 1) _slotCount.text = itemInstance._count.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData)
        {
            case { button: PointerEventData.InputButton.Left }:
                _inventoryActions.InvokeOnIsDragging((isDragging) =>
                {
                    if (!isDragging)
                    {
                    }
                    else
                    {

                    }
                });
                break;

            case { button: PointerEventData.InputButton.Right }:
                _inventoryActions.InvokeOnIsDragging((isDragging) =>
                {
                    if (!isDragging)
                    {

                    }
                    else
                    {

                    }
                });
                break;
        }
    }
}
