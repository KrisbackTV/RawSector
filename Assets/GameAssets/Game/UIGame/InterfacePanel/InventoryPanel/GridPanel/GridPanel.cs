using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

public class GridPanel : MonoBehaviour, IPointerDownHandler, IDropItemInWorld
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InventoryActions inventoryActions)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
    }

    private Background _background;
    private Container _conteiner;

    public int _width = 1;
    public int _height = 1;

    public InventoryData CurrentInventoryData { get; private set; }


    private void OnValidate()
    {
        if(_width < 1) _width = 1;
        if(_height < 1) _height = 1;

        var back = GetComponentInChildren<Background>();

        if (back != null)
        {
            back.SetSizeForInspector(_width, _height);
        }
    }


    private void Awake()
    {
        
        _background = GetComponentInChildren<Background>();
        _conteiner = GetComponentInChildren<Container>();
    }

    public void RefreshGrid(InventoryData inventoryData)
    {
        if(inventoryData == null) return;

        CurrentInventoryData = inventoryData;

        _conteiner.CleanConteiner();
        _background.SetSize(inventoryData.Width, inventoryData.Height);

        if (inventoryData._itemPosition != null)
        {
            foreach (KeyValuePair<ItemInstance, Vector2Int> item in inventoryData._itemPosition)
            {
                ItemInstance itemInstance = item.Key;
                Vector2Int position = item.Value;

                if(itemInstance ==  null) continue;

                _conteiner.AddViewItem(itemInstance, inventoryData, position.x, position.y);
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData)
        {
            case { button: PointerEventData.InputButton.Left }:

                _inventoryActions.InvokeOnIsDragging((isDragging) =>
                {
                    Vector2 mousePos = Mouse.current.position.ReadValue();
                    
                    if (!isDragging)
                    {
                        ItemInstance itemInstance = null;
                        _inventoryActions.InvokeOnGetItemForPosition(CurrentInventoryData, TryGetCellFromMouse(mousePos), (item) => itemInstance = item);

                        if (itemInstance != null) _inventoryActions.InvokeOnStartDrag(itemInstance, CurrentInventoryData, false);
                    }
                    else
                    {
                        Vector2Int position = TryGetCellFromMouse(mousePos);
                        _inventoryActions.InvokeOnEndDrag(CurrentInventoryData, position);
                    }
                });
                
                break;

            case { button: PointerEventData.InputButton.Right }:

                _inventoryActions.InvokeOnIsDragging((isDragging) =>
                {
                    Vector2 mousePos = Mouse.current.position.ReadValue();

                    if (!isDragging)
                    {
                        ItemInstance itemInstance = null;
                        _inventoryActions.InvokeOnGetItemForPosition(CurrentInventoryData, TryGetCellFromMouse(mousePos), (item) => itemInstance = item);

                        if (itemInstance != null)
                        {
                            if (itemInstance._count > 1)
                            {
                                int countDrag = itemInstance._count / 2;
                                
                                ItemInstance copyItem = new ItemInstance(itemInstance._itemData, countDrag);
                                itemInstance._count -= countDrag;

                                _inventoryActions.InvokeOnStartDrag(copyItem, CurrentInventoryData, true);
                                CurrentInventoryData.NotifyChange();
                                return;
                            }
                          
                            _inventoryActions.InvokeOnStartDrag(itemInstance, CurrentInventoryData, false);

                        }
                    }
                    else
                    {
                        ItemInstance draggedItem = null;
                        _inventoryActions.InvokeOnGetCurrentDragItem((item) => draggedItem = item);
                        if (draggedItem == null) return;

                        ItemInstance itemInstance = null;
                        Vector2Int itemPos = TryGetCellFromMouse(mousePos);
                        _inventoryActions.InvokeOnGetItemForPosition(CurrentInventoryData, itemPos, (item) => itemInstance = item);


                        if (itemInstance != null)
                        {
                            if (draggedItem._itemData.id == itemInstance._itemData.id)
                            {
                                if (draggedItem._count > 1)
                                {
                                    ItemInstance singleItem = new ItemInstance(draggedItem._itemData, 1);
                                
                                    _inventoryActions.InvokeOnTryPlaceItem(CurrentInventoryData, singleItem, itemPos.x, itemPos.y, (callback) =>
                                    {
                                        if (callback)
                                        {
                                            draggedItem._count--;
                                        
                                            _inventoryActions.InvokeOnShowGhostItem(draggedItem);
                                            CurrentInventoryData.NotifyChange();
                                        }
                                        else
                                        {
                                            Debug.Log("callback == false");
                                        }
                                    });
                                }
                                else
                                {
                                    _inventoryActions.InvokeOnEndDrag(CurrentInventoryData, itemPos);
                                }
                            }
                        }
                        else
                        {
                            if (draggedItem._count > 1)
                            {
                                ItemInstance singleItem = new ItemInstance(draggedItem._itemData, 1);

                                _inventoryActions.InvokeOnTryPlaceItem(CurrentInventoryData, singleItem, itemPos.x, itemPos.y, (callback) =>
                                {
                                    if (callback)
                                    {
                                        draggedItem._count--;

                                        _inventoryActions.InvokeOnShowGhostItem(draggedItem);
                                        CurrentInventoryData.NotifyChange();
                                    }
                                });
                            }
                            else
                            {
                                _inventoryActions.InvokeOnEndDrag(CurrentInventoryData, itemPos);
                            }
                        }
                    }
                });

                break;
        }
    }

    private Vector2Int TryGetCellFromMouse(Vector2 mousePos)
    {
        RectTransform rectTransform = _conteiner.GetComponent<RectTransform>();

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, null, out Vector2 localPoint))
        {
            float cellSize = _assetsLibrarySO.cellSize;

            float posX = localPoint.x;
            float posY = -localPoint.y;

            int x = Mathf.FloorToInt(posX / cellSize);
            int y = Mathf.FloorToInt(posY / cellSize);

            if (x >= 0 && x < CurrentInventoryData.Width && y >= 0 && y < CurrentInventoryData.Height)
            {
                return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);
    }

    public ItemInstance GetItemDropInWorld()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2Int itemPos = TryGetCellFromMouse(mousePos);

        ItemInstance itemInstance = null;
        _inventoryActions.InvokeOnGetItemForPosition(CurrentInventoryData, itemPos, (callback) =>
        {
            if (callback != null)
            {
                itemInstance = callback;
                _inventoryActions.InvokeOnRemoveItem(CurrentInventoryData, itemInstance, null);
            }
        });

        return itemInstance;
    }
}
