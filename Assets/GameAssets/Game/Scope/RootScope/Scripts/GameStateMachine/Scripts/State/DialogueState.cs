using UnityEngine;

[CreateAssetMenu(fileName = "DialogueState", menuName = "Architecture/States/DialogueState")]
public class DialogueState : GameState
{
    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;


        //_inputService.SwitchControlMap(GameMachineState.Dialogue);

        //_gameActions.InvokeOnOpenUIWindowForType(UIWindowType.Dialogue);

        //GameLog.LogInfo("State - Dialogue");
    }

    private void EscapePressed()
    {

    }

    public override void ExitState()
    {
        //_gameActions.OnEscPressed -= EscapePressed;
    }
}
