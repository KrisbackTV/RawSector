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
    public ItemInstance _draggedItem { get; private set; }
    public IItemSource _sourceInventory { get; private set; }


    private void GetCurrentDragItem(Action<ItemInstance> callback)
    {
        callback?.Invoke(_draggedItem);
    }

    public void StartDrag(ItemInstance item, IItemSource inventory, bool isCopyItem)
    {
        if (item == null || inventory == null) return;

        _draggedItem = item;
        _sourceInventory = inventory;
        IsDragging = true;
        _inventoryActions.InvokeOnShowGhostItem(_draggedItem);

        if (!isCopyItem)
        {
            inventory.Remove(item, null);
        }
    }

    public void EndDrag(IItemSource inventoryData)
    {
        if (!IsDragging || _draggedItem == null || inventoryData == null) return;

        Debug.Log("enddrag");

        inventoryData.Receive(_draggedItem, (isSuccess) =>
         {
             if (isSuccess)
             {
                 FinishDragging();
             }
             else
             {
                 _inventoryActions.InvokeOnDropItemInWorld(_draggedItem);
                 FinishDragging();
             }
         });
    }

    public void CancelDrag()
    {
        if (!IsDragging) return;

        _sourceInventory.Receive(_draggedItem, (isSuccess) =>
        {
            if (isSuccess)
            {
                FinishDragging();
                return;
            }
            else
            {
                _inventoryActions.InvokeOnDropItemInWorld(_draggedItem);
                FinishDragging();
                return;
            }
        });

        FinishDragging();
    }

    private void FinishDragging()
    {
        _draggedItem = null;
        _sourceInventory = null;
        IsDragging = false;
        _inventoryActions.InvokeOnHideGhostItem();
    }

    private void IsDragged(Action<bool> callback)
    {
        callback?.Invoke(IsDragging);
    }
}
