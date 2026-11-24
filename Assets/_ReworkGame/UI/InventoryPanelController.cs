using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField] private UIItemList _containerItemList;
    [SerializeField] private UIItemList _backpackItemList;
    private CanvasGroup _canvasGroup;


    private InventoryData _playerData;
    private InventoryData _containerData;
    private bool _isOpen = false;


    public void Initialize()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        SetActive(_isOpen);

        Subscribe();
    }


    public void OpenInventory(InventoryUIContext ctx)
    {
        UnSubscribe();

        if (_isOpen == false)
        {
            if (ctx != null)
            {
                if (ctx._playerData != null)
                {
                    List<ItemInstance> list = ctx._playerData._itemList;
                    if (_backpackItemList != null)
                        _backpackItemList.UpdateList(list);

                    ctx._playerData.OnChanged += _backpackItemList.UpdateList;
                    _playerData = ctx._playerData;
                }
                else
                    _backpackItemList.UpdateList(null);

                if (ctx._containerData != null)
                {
                    List<ItemInstance> list = ctx._containerData._itemList;
                    if (_containerItemList != null)
                            _containerItemList.UpdateList(list);

                    ctx._containerData.OnChanged += _containerItemList.UpdateList;
                }
                else
                {
                    _containerItemList.UpdateList(null);
                }
            }

            SetActive(true);
        }
    }

    public void CloseInventory()
    {
        SetActive(false);
    }


    private void SetActive(bool value)
    {
        if (_canvasGroup == null) return;

        if (!value)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;

            _isOpen = false;
        }
        else
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;

            _isOpen = true;
        }
    }

    private void Subscribe()
    {
    }

    private void UnSubscribe()
    {
        if (_playerData != null)
                _playerData.OnChanged -= _backpackItemList.UpdateList;

        if(_containerData != null)
                _containerData.OnChanged -= _containerItemList.UpdateList;
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}
