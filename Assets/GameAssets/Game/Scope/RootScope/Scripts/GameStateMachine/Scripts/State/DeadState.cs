using UnityEngine;


[CreateAssetMenu(fileName = "DeadState", menuName = "Architecture/States/DeadState")]
public class DeadState : GameState
{
    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;


        //_inputService.SwitchControlMap(GameMachineState.Dead);
        //_gameActions.InvokeOnOpenUIWindowForType(UIWindowType.Dead);

        //GameLog.LogInfo("State - Dead");
    }

    private void EscapePressed()
    {
    }

    public override void ExitState()
    {
        //_gameActions.OnEscPressed -= EscapePressed;
    }
}
