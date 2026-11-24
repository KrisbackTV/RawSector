

public class GameStarter
{
    private GameStateMachine _stateMachine;
    private AssetsLibrarySO _assetsLibrarySO;

    public GameStarter(GameStateMachine stateMachine, AssetsLibrarySO assetsLibrarySO)
    {
        if (stateMachine == null || assetsLibrarySO == null)
        {
            string isMachine = stateMachine != null ? "присутствует" : "остутствует";
            string isLibrary = assetsLibrarySO != null ? "присутствует" : "остутствует";

            GameLog.LogError($"Какойто из компонентов отсутствует (StateMachine:{isMachine}, AssetsLibrary:{isLibrary})");
        }

        _stateMachine = stateMachine;
        _assetsLibrarySO = assetsLibrarySO;

        GameLog.LogInfo("Стартер успешно создан");
    }

    public void Start()
    {
        _stateMachine.ChangeState(GameMachineState.Menu);
    }
}
