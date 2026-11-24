using System;
using UnityEngine;

public class DragAndDropService
{
    
    public void Start()
    {
        //if (_gameActions != null)
        //{
        //    _gameActions.OnStartDrag += StartDrag;
        //    _gameActions.OnCancelDrag += CancelDrag;
        //    _gameActions.OnEndDrag += EndDrag;

        //    _gameActions.OnGetCurrentDragItem += GetCurrentDragItem;
        //    _gameActions.OnIsDragging += IsDragged;

        //    GameLog.LogInfo("Cервис успешно создан");
        //}
    }

    public bool IsDragging { get; private set; } = false;
    public DragContextItem _draggedItemContext { get; private set; }


    private void GetCurrentDragItem(Action<DragContextItem> callback)
    {
        callback?.Invoke(_draggedItemContext);
    }

    public void StartDrag(ItemInstance item, IItemSource inventory, bool isCopyItem)
    {
        if (item == null || inventory == null) return;

        _draggedItemContext = new DragContextItem(item, inventory);
        IsDragging = true;
        //_gameActions.InvokeOnShowGhostItem(_draggedItemContext._itemInstance);

        if (!isCopyItem)
        {
            inventory.Remove(item, null);
        }
    }

    public void EndDrag(IItemSource inventoryData)
    {
        //if (!IsDragging || _draggedItemContext == null || inventoryData == null) return;

        //inventoryData.Receive(_draggedItemContext, (isSuccess) =>
        // {
        //     if (isSuccess)
        //     {
        //         FinishDragging();
        //     }
        //     else
        //     {
        //         _draggedItemContext._inventoryOwner.Sourse(_draggedItemContext, (callback) => 
        //         {
        //             if (!callback)
        //             {
        //                 FinishDragging();
        //                 return;
        //             }
        //         });

        //         Vector3 dropPos = _playerData._playerRuntimeData.PlayerPosition + _playerData._playerRuntimeData.Forward * 1.2f;
        //         _gameActions.InvokeOnDropItemInWorld(_draggedItemContext._itemInstance, dropPos, Quaternion.identity);
        //         FinishDragging();
        //     }
        // });
    }

    public void CancelDrag()
    {
        //if (!IsDragging) return;

        //_draggedItemContext._inventoryOwner.Sourse(_draggedItemContext, (isSuccess) =>
        //{
        //    if (isSuccess)
        //    {
        //        FinishDragging();
        //        return;
        //    }
        //    else
        //    {
        //        Vector3 dropPos = _playerData._playerRuntimeData.PlayerPosition + _playerData._playerRuntimeData.Forward * 1.2f;

        //        _gameActions.InvokeOnDropItemInWorld(_draggedItemContext._itemInstance, dropPos, Quaternion.identity);
        //        FinishDragging();
        //        return;
        //    }
        //});

        //FinishDragging();
    }

    private void FinishDragging()
    {
        _draggedItemContext = null;
        IsDragging = false;
        //_gameActions.InvokeOnHideGhostItem();
    }

    private void IsDragged(Action<bool> callback)
    {
        callback?.Invoke(IsDragging);
    }
}
