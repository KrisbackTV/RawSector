using UnityEngine;

public class InventoryPanelWiew : MonoBehaviour, IWindow
{
    [SerializeField] private GridPanel _inventoryPanel;

    private CanvasGroup _canvasGroup;
    public GameObject _gameObject => gameObject;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        ActivePanel(true);
    }

    public void Close()
    {
        ActivePanel(false);
    }

    private void ActivePanel(bool value)
    {
        if (_canvasGroup == null) return;

        _canvasGroup.alpha = value ? 1 : 0;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    public void UpdateInventoryPanel(InventoryData inventoryData)
    {
        _inventoryPanel.RefreshGrid(inventoryData);
    }
}
