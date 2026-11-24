using UnityEngine;

[CreateAssetMenu(fileName = "MenuState", menuName = "Architecture/States/MenuState")]
public class MenuState : GameState
{
    public override void EnterState()
    {
        //_gameActions.OnEscPressed += EscapePressed;

        //_sceneLoader.BackToMenu();

        //_inputService.SwitchControlMap(GameMachineState.Menu);
        //_gameActions.InvokeOnOpenMenuWindowForType(MenuWindow.MainMenu);

        //GameLog.LogInfo("State - Menu");
    }

    private void EscapePressed()
    {

    }

    public override void ExitState()
    {
        //_gameActions.OnEscPressed -= EscapePressed;
    }
}