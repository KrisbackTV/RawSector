using UnityEngine;

[CreateAssetMenu(fileName = "GameplayState", menuName = "Architecture/States/Gameplay")]
public class GameplayState : GameState
{

    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;
     

        //_inputService.SwitchControlMap(GameMachineState.Gameplay);

        //_gameActions.InvokeOnOpenUIWindowForType(UIWindowType.HUD);

        //GameLog.LogInfo("State - Gameplay");
    }

    private void EscapePressed()
    {
        //_gameActions.InvokeOnPushStateForGame(GameMachineState.EscMenu);
    }

    public override void ExitState() 
    {
        //_gameActions.OnEscPressed -= EscapePressed;
    }
}