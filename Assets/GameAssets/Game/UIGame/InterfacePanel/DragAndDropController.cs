using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropController : MonoBehaviour
{

    [SerializeField] private Transform _ghostCanvasTransform;
    private GhostItemView _activeGhostItem;

    private GraphicRaycaster _raycaster;


    private void Start()
    {
        _raycaster = GetComponentInParent<GraphicRaycaster>();

        if (_raycaster == null) Debug.Log("raycaster == null");

        //if (_assetsLibrarySO.UIGhostItemViewPrefab != null && _ghostCanvasTransform != null)
        //{
        //    //_activeGhostItem = _objectResolver.Instantiate(_assetsLibrarySO.UIGhostItemViewPrefab, _ghostCanvasTransform).GetComponent<GhostItemView>();
        //    //_activeGhostItem.Hide();
        //}

        Subscribe();
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
        //if (_inventoryActions != null)
        //{
        //    _inventoryActions.OnShowGhostItem += ShowGhostItem;
        //    _inventoryActions.OnHideGhostItem += HideGhostItem;
        //}
    }


    private void UnSubscribe()
    {
        //if (_inventoryActions != null)
        //{
        //    _inventoryActions.OnShowGhostItem -= ShowGhostItem;
        //    _inventoryActions.OnHideGhostItem -= HideGhostItem;
        //}
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
