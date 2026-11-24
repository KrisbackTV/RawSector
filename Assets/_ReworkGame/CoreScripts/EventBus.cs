using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<InventoryData> OnContainerOpened;
    public static void OpenContainer(InventoryData data) => OnContainerOpened?.Invoke(data);


    public static event Action<InventoryUIContext> OnInventoryUIOpened;
    public static void OpenInventoryUI(InventoryUIContext context) => OnInventoryUIOpened?.Invoke(context);


    public static event Action OnCloseVindow;
    public static void CloseInventoryUI() => OnCloseVindow?.Invoke();
}
