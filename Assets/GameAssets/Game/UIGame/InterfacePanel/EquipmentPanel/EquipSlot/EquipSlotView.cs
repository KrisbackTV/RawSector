using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

public class EquipSlotView : MonoBehaviour, IPointerDownHandler
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;
    private PlayerData _playerData;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InventoryActions inventoryActions, PlayerData playerData)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
        _playerData = playerData;

        if (_inventoryActions != null)
        {
            _inventoryActions.OnUpdateToEquipmentSlot += UpdateSlotView;
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

        if (itemInstance != null)
        {
            if (_slotImage != null)
            {
                    _slotImage.sprite = itemInstance._itemSprite;
                    _slotImage.color = Color.white;
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
        else
        {
            if (_slotImage != null)
            {
                _slotImage.sprite = null;
                _slotImage.color = new Color32(32,32,32,255);
            }

            if (_slotName != null)
            {
                _slotName.text = "";
            }

            if (_slotCount != null)
            {
                _slotCount.text = "";
            }
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
                        _inventoryActions.InvokeOnStartDrag(_playerData._equipmentSO.GetItemToSlot(_slotType), _playerData._equipmentSO, false);
                    }
                    else
                    {
                        _inventoryActions.InvokeOnGetItemForEquipSlot(_slotType, (item) =>
                        {
                            if (item == null)
                            {
                                _inventoryActions.InvokeOnGetCurrentDragItem((draggItem) =>
                                {
                                    if (draggItem._itemInstance._itemData.equipmentType == _slotType)
                                    {
                                        _inventoryActions.InvokeOnEndDrag(_playerData._equipmentSO);
                                    }
                                });
                            }
                        });
                    }
                });
                break;

            case { button: PointerEventData.InputButton.Right }:
                break;
        }
    }
}
