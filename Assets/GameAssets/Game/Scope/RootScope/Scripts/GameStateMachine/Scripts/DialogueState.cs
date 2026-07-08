using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "DialogueState", menuName = "Architecture/States/DialogueState")]
public class DialogueState : GameState
{
    private AssetsLibrarySO _assetsLibrarySO;
    private SceneLoader _sceneLoader;
    private InputService _inputService;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibraryOS, SceneLoader sceneLoader, InputService inputService)
    {
        _assetsLibrarySO = assetsLibraryOS;
        _sceneLoader = sceneLoader;
        _inputService = inputService;

    }

    public override void EnterState()
    {
        _assetsLibrarySO.OnEscapePressed += EscapePressed;


        _inputService.SwitchControlMap(GameCoreState.Dialogue);

        _assetsLibrarySO.OnOpenWindowForType?.Invoke(WindowType.Dialogue);

        Debug.Log("State - Dialogue");
    }

    private void EscapePressed()
    {

    }

    public override void ExitState()
    {
        _assetsLibrarySO.OnEscapePressed -= EscapePressed;
    }
}
