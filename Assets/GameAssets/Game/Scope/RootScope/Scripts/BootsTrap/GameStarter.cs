using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameStarter : IStartable
{
    private GameStateMachine _stateMachine;
    private AssetsLibrarySO _assetsLibrarySO;

    public GameStarter(GameStateMachine stateMachine, AssetsLibrarySO assetsLibrarySO)
    {
        _stateMachine = stateMachine;
        _assetsLibrarySO = assetsLibrarySO;
    }

    public void Start()
    {
        _stateMachine.ChangeState(_assetsLibrarySO.GetGameStateForType(GameCoreState.Menu));
    }
}
