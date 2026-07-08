using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using static UnityEngine.Rendering.DebugUI;

public enum WindowType
{
    HUD,
    Inventory,
    EscMenu,
    Dialogue
}

public class UIManager : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrary;
    private InputService _inputService;

    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InputService inputService)
    {
        _assetsLibrary = assetsLibrarySO;
        _inputService = inputService;

    }

    [SerializeField] private CanvasGroup _backgroundPanel;
    [SerializeField] private InterfacePanelWiew _interfacePanelWiew;
    [SerializeField] private InventoryPanelWiew _inventoryPanelWiew;
    [SerializeField] private HUDPanel _hudPanel;
    [SerializeField] private EscapeMenu _escapeMenu;

    private void Awake()
    {
        _assetsLibrary.OnOpenWindowForType += OpenWindow;
    }

    private void Start()
    {
        ActiveBackgroundPanel(false);
        _interfacePanelWiew.Close();
        _inventoryPanelWiew.Close();
        _hudPanel.Close();
        _escapeMenu.Close();

        OpenWindow(WindowType.HUD);
    }

    private void OpenWindow(WindowType type)
    {
        ActiveBackgroundPanel(false);
        _interfacePanelWiew.Close();
        _inventoryPanelWiew.Close();
        _hudPanel.Close();
        _escapeMenu.Close();

        _assetsLibrary.OnCloseUIWindow?.Invoke();

        switch (type)
        {
            case WindowType.HUD:
                _hudPanel.Open();
                SetLockCursor(true);
                break;

            case WindowType.Inventory:
                ActiveBackgroundPanel(true);
                _interfacePanelWiew.Open();
                _inventoryPanelWiew.Open();
                SetLockCursor(false);
                break;

            case WindowType.EscMenu:
                _escapeMenu.Open();
                SetLockCursor(false);
                break;
        }
    }

    private void SetLockCursor(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !value;
    }

    private void ActiveBackgroundPanel(bool active)
    {
        if (_backgroundPanel == null) return;

        _backgroundPanel.alpha = active ? 1 : 0;
        _backgroundPanel.interactable = active;
        _backgroundPanel.blocksRaycasts = active;
    }

    private void UnSubscribe()
    {
        _assetsLibrary.OnOpenWindowForType -= OpenWindow;
    }

    private void OnDisable() => UnSubscribe();
    private void OnDestroy() => UnSubscribe();
}
