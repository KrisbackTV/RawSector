using UnityEngine;

public class GameCore
{
    private AssetsLibrarySO _assetsLibrarySO;
    private SceneLoader _sceneLoader;
    private GameStateMachine _gameStateMachine;
    private InputService _inputService;

    public GameCore(AssetsLibrarySO assetsLibrarySO, SceneLoader sceneLoader, GameStateMachine gameStateMachine, InputService inputService)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
        _inputService = inputService;

        _assetsLibrarySO.OnOpenInventoryEvent += OpenInventory;

        _assetsLibrarySO.OnChangeStateEvent += ChangeStateForGame;
        _assetsLibrarySO.OnPushStateEvent += PushStateForGame;
        _assetsLibrarySO.OnPopStateEvent += PopStateForGame;
    }

    private void ChangeStateForGame(GameCoreState state)
    {
        _gameStateMachine.ChangeState(_assetsLibrarySO.GetGameStateForType(state));
    }

    private void PopStateForGame()
    {
        _gameStateMachine.PopState();
    }

    private void PushStateForGame(GameCoreState state)
    {
        _gameStateMachine.PushState(_assetsLibrarySO.GetGameStateForType(state));
    }

public void StartNewGame()
    {
        _gameStateMachine.ChangeState(_assetsLibrarySO.GetGameStateForType(GameCoreState.Gameplay));

        if (_assetsLibrarySO._testSceneForGame)
        {
            _sceneLoader.LoadScene("TestGameScene");
        }
        else
        {
            _sceneLoader.LoadScene("TestMup1");
        }
    }

    public void QuicToMenu()
    {
        _gameStateMachine.PushState(_assetsLibrarySO.GetGameStateForType(GameCoreState.Menu));
    }

    public void OpenInventory(InventoryData inventoryData)
    {
        _assetsLibrarySO.OnPushStateEvent(GameCoreState.Inventory);
    }

    public void CloseInventory()
    {
        _gameStateMachine.PopState();
    }
}
