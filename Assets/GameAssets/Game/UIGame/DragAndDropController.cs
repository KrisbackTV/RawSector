using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class DragAndDropController : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;
    private InputService _inputService;
    private IObjectResolver _objectResolver;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, EquipmentService equipmentService, InventoryActions inventoryActions, InputService inputService, IObjectResolver objectResolver)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
        _inputService = inputService;
        _objectResolver = objectResolver;
    }

    [SerializeField] private Transform _ghostCanvasTransform;
    private GhostItemView _activeGhostItem;

    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;


    private void Start()
    {
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _eventSystem = EventSystem.current;

        if (_raycaster == null) Debug.Log("raycaster == null");

        if (_assetsLibrarySO.UIGhostItemViewPrefab != null && _ghostCanvasTransform != null)
        {
            _activeGhostItem = _objectResolver.Instantiate(_assetsLibrarySO.UIGhostItemViewPrefab, _ghostCanvasTransform).GetComponent<GhostItemView>();
            _activeGhostItem.Hide();
        }

        Subscribe();
    }

    private void DropItemInWorld()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult hit in results)
        {
            if (hit.gameObject.GetComponentInParent<IDropItemInWorld>() != null)
            {
                IDropItemInWorld component = hit.gameObject.GetComponentInParent<IDropItemInWorld>();

                ItemInstance item = component.GetItemDropInWorld();
                _inventoryActions.InvokeOnDropItemInWorld(item);
            }
        }
    }

    private void ShowGhostItem(ItemInstance item)
    {
        _activeGhostItem.Show(item);
    }

    private void HideGhostItem()
    {
        _activeGhostItem.Hide();
    }

    private void Subscribe()
    {
        if (_inventoryActions != null)
        {
            _inputService.OnDropItem += DropItemInWorld;

            _inventoryActions.OnShowGhostItem += ShowGhostItem;
            _inventoryActions.OnHideGhostItem += HideGhostItem;
        }
    }


    private void UnSubscribe()
    {
        if (_inventoryActions != null)
        {
            _inputService.OnDropItem -= DropItemInWorld;

            _inventoryActions.OnShowGhostItem -= ShowGhostItem;
            _inventoryActions.OnHideGhostItem -= HideGhostItem;
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
