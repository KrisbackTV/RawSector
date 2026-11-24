using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridPanel : MonoBehaviour, IPointerDownHandler, IDropItemInWorld
{

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
        //if(inventoryData == null) return;

        //CurrentInventoryData = inventoryData;

        //_conteiner.CleanConteiner();
        //_background.SetSize(inventoryData.Width, inventoryData.Height);

        //if (inventoryData._itemPosition != null)
        //{
        //    foreach (KeyValuePair<ItemInstance, Vector2Int> item in inventoryData._itemPosition)
        //    {
        //        ItemInstance itemInstance = item.Key;
        //        Vector2Int position = item.Value;

        //        if(itemInstance ==  null) continue;

        //        _conteiner.AddViewItem(itemInstance, inventoryData, position.x, position.y);
        //    }
        //}
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //switch (eventData)
        //{
        //    case { button: PointerEventData.InputButton.Left }:

        //        _inventoryActions.InvokeOnIsDragging((isDragging) =>
        //        {
        //            Vector2 mousePos = Mouse.current.position.ReadValue();
                    
        //            if (!isDragging)
        //            {
        //                ItemInstance itemInstance = null;
        //                _inventoryActions.InvokeOnGetItemForPositionToInventory(CurrentInventoryData, TryGetCellFromMouse(mousePos), (item) => itemInstance = item);

        //                if (itemInstance != null) _inventoryActions.InvokeOnStartDrag(itemInstance, CurrentInventoryData, false);
        //            }
        //            else
        //            {
        //                Vector2Int position = TryGetCellFromMouse(mousePos);
        //                Debug.Log($"mousepo sition: {position}");

        //                DragContextItem dragContextItem = null;
        //                _inventoryActions.InvokeOnGetCurrentDragItem((callback) => dragContextItem = callback);
        //                dragContextItem._posInventoryGrid = position;
        //                Debug.Log($"dragItem pos for inventory: {dragContextItem._posInventoryGrid}");

        //                _inventoryActions.InvokeOnEndDrag(CurrentInventoryData);
        //                Debug.Log($"the end");
        //            }
        //        });
        //        break;
        //    case { button: PointerEventData.InputButton.Right }:

        //        _inventoryActions.InvokeOnIsDragging((isDragging) =>
        //        {
        //            Vector2 mousePos = Mouse.current.position.ReadValue();
                    
        //            if (!isDragging)
        //            {
        //                ItemInstance itemInstance = null;
        //                _inventoryActions.InvokeOnGetItemForPositionToInventory(CurrentInventoryData, TryGetCellFromMouse(mousePos), (item) => itemInstance = item);

        //                if (itemInstance != null)
        //                {
        //                    DragContextItem contextItem = new DragContextItem(itemInstance, CurrentInventoryData);
        //                    _inventoryActions.InvokeOnOpenInteractMenu(contextItem, mousePos);
        //                }
        //            }
        //            else
        //            {

        //            }
        //        });
        //        break;
        //}
    }

    private Vector2Int TryGetCellFromMouse(Vector2 mousePos)
    {
        //RectTransform rectTransform = _conteiner.GetComponent<RectTransform>();

        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, null, out Vector2 localPoint))
        //{
        //    float cellSize = _assetsLibrarySO.cellSize;

        //    float posX = localPoint.x;
        //    float posY = -localPoint.y;

        //    int x = Mathf.FloorToInt(posX / cellSize);
        //    int y = Mathf.FloorToInt(posY / cellSize);

        //    if (x >= 0 && x < CurrentInventoryData.Width && y >= 0 && y < CurrentInventoryData.Height)
        //    {
        //        return new Vector2Int(x, y);
        //    }
        //}

        return new Vector2Int(-1, -1);
    }

    public ItemInstance GetItemDropInWorld()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2Int itemPos = TryGetCellFromMouse(mousePos);

        ItemInstance itemInstance = null;
        //_inventoryActions.InvokeOnGetItemForPositionToInventory(CurrentInventoryData, itemPos, (callback) =>
        //{
        //    if (callback != null)
        //    {
        //        itemInstance = callback;
        //        _inventoryActions.InvokeOnRemoveItemForInventory(CurrentInventoryData, itemInstance, null);
        //    }
        //});

        return itemInstance;
    }
}
