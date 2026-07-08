using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

[CreateAssetMenu(fileName = "MenuState", menuName = "Architecture/States/MenuState")]
public class MenuState : GameState
{
    private AssetsLibrarySO _assetsLibrarySO;
    private SceneLoader _sceneLoader;
    private InputService _inputService;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, SceneLoader sceneLoader, InputService inputService)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _sceneLoader = sceneLoader;
        _inputService = inputService;
    }

    public override void EnterState()
    {
        _assetsLibrarySO.OnEscapePressed += EscapePressed;

        _sceneLoader.BackToMenu();

        _inputService.SwitchControlMap(GameCoreState.Menu);

        Debug.Log("State - Menu");
    }

    private void EscapePressed()
    {

    }

    public override void ExitState()
    {
        _assetsLibrarySO.OnEscapePressed -= EscapePressed;
    }
}