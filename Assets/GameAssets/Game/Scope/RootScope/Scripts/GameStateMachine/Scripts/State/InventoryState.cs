using UnityEngine;


[CreateAssetMenu(fileName = "InventoryState", menuName = "Architecture/States/InventoryState")]
public class InventoryState : GameState
{

    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;


        //_inputService.SwitchControlMap(GameMachineState.Inventory);
        //_gameActions.InvokeOnOpenUIWindowForType(UIWindowType.Inventory);

        //GameLog.LogInfo("State - Inventory");
    }

    private void EscapePressed()
    {
        //_gameActions.InvokeOnPopStateForGame();
    }

    public override void ExitState()
    {
        //_gameActions.OnEscPressed -= EscapePressed;
        //_gameActions.InvokeOnCancelDrag();
    }
}
