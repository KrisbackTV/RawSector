using UnityEngine;


[CreateAssetMenu(fileName = "SaveLoadMenuState", menuName = "Architecture/States/SaveLoadMenuState")]
public class SaveLoadMenuState : GameState
{
    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;


        //_inputService.SwitchControlMap(GameMachineState.SaveLoadMenu);
        

        GameLog.LogInfo("State - SaveLoadMenu");
    }

    private void EscapePressed()
    {
        //_gameActions.InvokeOnPopStateForGame();
    }

    public override void ExitState()
    {
        //_gameActions.OnEscPressed -= EscapePressed;
    }
}
