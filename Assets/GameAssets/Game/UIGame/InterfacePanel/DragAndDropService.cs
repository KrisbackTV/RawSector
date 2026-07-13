using System;
using UnityEngine;
using VContainer.Unity;

public class DragAndDropService : IStartable
{
    private InventoryActions _inventoryActions;

    public DragAndDropService(InventoryActions inventoryActions)
    {
        _inventoryActions = inventoryActions;
    }

    public void Start()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnStartDrag += StartDrag;
            _inventoryActions.OnCancelDrag += CancelDrag;
            _inventoryActions.OnEndDrag += EndDrag;

            _inventoryActions.OnGetCurrentDragItem += GetCurrentDragItem;
            _inventoryActions.OnIsDragging += IsDragged;
        }
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
        _inventoryActions.InvokeOnShowGhostItem(_draggedItemContext._itemInstance);

        if (!isCopyItem)
        {
            inventory.Remove(item, null);
        }
    }

    public void EndDrag(IItemSource inventoryData)
    {
        if (!IsDragging || _draggedItemContext == null || inventoryData == null) return;

        inventoryData.Receive(_draggedItemContext, (isSuccess) =>
         {
             if (isSuccess)
             {
                 FinishDragging();
             }
             else
             {
                 _draggedItemContext._inventoryOwner.Sourse(_draggedItemContext, (callback) => 
                 {
                     if (!callback)
                     {
                         FinishDragging();
                         return;
                     }
                 });
                 _inventoryActions.InvokeOnDropItemInWorld(_draggedItemContext._itemInstance);
                 FinishDragging();
             }
         });
    }

    public void CancelDrag()
    {
        if (!IsDragging) return;

        _draggedItemContext._inventoryOwner.Receive(_draggedItemContext, (isSuccess) =>
        {
            if (isSuccess)
            {
                FinishDragging();
                return;
            }
            else
            {
                _inventoryActions.InvokeOnDropItemInWorld(_draggedItemContext._itemInstance);
                FinishDragging();
                return;
            }
        });

        FinishDragging();
    }

    private void FinishDragging()
    {
        _draggedItemContext = null;
        IsDragging = false;
        _inventoryActions.InvokeOnHideGhostItem();
    }

    private void IsDragged(Action<bool> callback)
    {
        callback?.Invoke(IsDragging);
    }
}
