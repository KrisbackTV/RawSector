using UnityEngine;

public class OpenInventory : MonoBehaviour, IEntityModule
{
    private IInventoryProvider _inventoryData;
    private IInputProvider _inputProvider;

    public void OnEntityInitializer(IEntityContext context)
    {
        _inventoryData = context.InventoryData;
        _inputProvider = context.InputProvider;

        Subscribe();
    }


    private void OnPlayerInventory()
    {
        //if (_inventoryData.Inventory != null)
        //{
        //    _gameActions.InvokeOnPushStateForGame(GameMachineState.Inventory);
        //    _gameActions.InvokeOnOpenInventory(_inventoryData.Inventory);
        //}


    }



    private void Subscribe()
    {
        _inputProvider.OnOpenInventory += OnPlayerInventory;
    }

    private void UnSubscriber()
    {
        _inputProvider.OnOpenInventory -= OnPlayerInventory;
    }

    private void OnDisable() => UnSubscriber();
    private void OnDestroy() => UnSubscriber();
}
