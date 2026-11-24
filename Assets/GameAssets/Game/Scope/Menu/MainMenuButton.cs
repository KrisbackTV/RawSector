using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrary;
    private SceneLoader _sceneLoader;
    private GameStateMachine _gameStateMachine;
    public void Construct(SceneLoader sceneLoader, GameStateMachine gameStateMachine, AssetsLibrarySO assetsLibrary)
    {
        _sceneLoader = sceneLoader;
        _gameStateMachine = gameStateMachine;
        _assetsLibrary = assetsLibrary;
    }

    public void SaveLoadMenu()
    {
        if (_assetsLibrary._testSceneForGame)
        {
            _gameStateMachine.ChangeState(GameMachineState.Gameplay);
            GameLog.LogInfo("Запуск тестовой сцены");
        }
        else
        {
            //_gameStateMachine.PushState(GameMachineState.SaveLoadMenu);

            _gameStateMachine.ChangeState(GameMachineState.Gameplay);
            GameLog.LogInfo("Запуск игры");
        }
    }

    public void SettingMenu()
    {

    }

    public void ExitGame()
    {
        GameLog.LogInfo("Выход из игры...");

        // Если игра запущена в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        // Если это уже собранный билд игры (ПК, андроид и т.д.)
        Application.Quit();
    }
}
