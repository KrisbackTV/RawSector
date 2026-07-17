using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class InteractionMenuController : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private GameActions _inventoryActions;
    private PlayerData _playerData;
    private IObjectResolver _objectResolver;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, EquipmentService equipmentService, GameActions inventoryActions, PlayerData playerData, IObjectResolver objectResolver)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
        _playerData = playerData;
        _objectResolver = objectResolver;
    }

    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private BackgroundInteractPanel _backgroundInteractPanel;
    private GameObject _interactMenu;

    private GameObject _prefabMenu;
    private GameObject _prefabButton;


    private void Start()
    {
        if (_assetsLibrarySO != null)
        { 
            _prefabMenu = _assetsLibrarySO._interactMenuPrefab;
            _prefabButton = _assetsLibrarySO._interactButtonPrefab;
        }

        if (_backgroundInteractPanel != null)
        {
            _backgroundInteractPanel.Close();
        }

        Subscribe();
    }


    private void OpenInteractMenu(DragContextItem contextItem, Vector2 mousePos)
    {
        if (_interactMenu != null || contextItem == null || contextItem._itemInstance == null) return;

        _interactMenu = _objectResolver.Instantiate(_prefabMenu, _canvasTransform);

        RectTransform menuRect = _interactMenu.GetComponent<RectTransform>();
        RectTransform canvasRect = _canvasTransform.GetComponent<RectTransform>();

        if (menuRect != null && canvasRect != null)
        {
            // Пересчитываем позицию мыши (Screen Point) в локальные координаты Canvas
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    mousePos,
                    null, // null подходит для Screen Space - Overlay. Если камера — передай камеру UI
                    out Vector2 localPoint))
            {
                // Устанавливаем точную позицию
                menuRect.anchoredPosition = localPoint;
            }
        }

        ItemActionType actionType = contextItem._itemInstance._itemData.ItemActionType;

        if (actionType.HasFlag(ItemActionType.Use))
        {
            AddButton("Использовать", null);
        }
        if (actionType.HasFlag(ItemActionType.Modified))
        {
            AddButton("Осмотреть", null);
        }
        if (actionType.HasFlag(ItemActionType.Discard))
        {
            AddButton("Выбросить", () =>
            {
                Vector3 dropPos = _playerData._playerRuntimeData.PlayerPosition + _playerData._playerRuntimeData.Forward * 1.2f;

                _inventoryActions.InvokeOnDropItemInWorld(contextItem._itemInstance, dropPos, Quaternion.identity);
                contextItem._inventoryOwner.Remove(contextItem._itemInstance, null);
            });
        }

        if (_backgroundInteractPanel != null) _backgroundInteractPanel.Open();
    }

    private void AddButton(string name, Action callback)
    {
        GameObject buttonObj = _objectResolver.Instantiate(_prefabButton, _interactMenu.transform);
        TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        if (text != null)
        {
            text.text = name;
        }

        Button button = buttonObj.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                callback?.Invoke();

                CloseInteractMenu();
            });
        }
    }

    private void CloseInteractMenu()
    {
        if (_interactMenu != null)
        {
            Destroy(_interactMenu.gameObject);
        }

        if (_backgroundInteractPanel != null) _backgroundInteractPanel.Close();
    }


    private void Subscribe()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnOpenInteractMenu += OpenInteractMenu;
            _inventoryActions.OnCloseInteractMenu += CloseInteractMenu;
        }
    }


    private void UnSubscribe()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnOpenInteractMenu -= OpenInteractMenu;
            _inventoryActions.OnCloseInteractMenu -= CloseInteractMenu;
        }
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }
}
