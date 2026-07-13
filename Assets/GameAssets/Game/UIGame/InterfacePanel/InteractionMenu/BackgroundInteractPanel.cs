using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class BackgroundInteractPanel : MonoBehaviour, IPointerDownHandler
{
    private InventoryActions _inventoryActions;
    [Inject]
    public void Construct(InventoryActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
        Debug.Log("ИНЖЕКТ");
    }


    private CanvasGroup _canvasGroup;
    public GameObject _gameObject => gameObject;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ActivePanel(bool value)
    {
        if (_canvasGroup == null) return;

        _canvasGroup.alpha = value ? 1 : 0;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    public void Open()
    {
        ActivePanel(true);
    }

    public void Close()
    {
        ActivePanel(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData)
        {
            case { button: PointerEventData.InputButton.Left }:
                if (_inventoryActions == null) Debug.Log("null");
                _inventoryActions.InvokeOnCloseInteractMenu();
                break;

            case { button: PointerEventData.InputButton.Right }:
                _inventoryActions.InvokeOnCloseInteractMenu();
                break;
        }

    }
}
