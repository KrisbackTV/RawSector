using UnityEngine;

[CreateAssetMenu(fileName = "EscMenuState", menuName = "Architecture/States/EscMenuState")]
public class EscMenuState : GameState
{

    public override void EnterState()
    {
        //    _gameActions.OnEscPressed += EscapePressed;


        //    _inputService.SwitchControlMap(GameMachineState.EscMenu);

        //    _gameActions.InvokeOnOpenUIWindowForType(UIWindowType.EscMenu);

        //    GameLog.LogInfo("State - EscMenu");
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
