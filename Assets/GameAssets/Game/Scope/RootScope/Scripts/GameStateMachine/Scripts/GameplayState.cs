using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "GameplayState", menuName = "Architecture/States/Gameplay")]
public class GameplayState : GameState
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InputService _inputService;

    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InputService inputService, GameCore gameCore)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputService = inputService;

    }

    public override void EnterState()
    {
        _assetsLibrarySO.OnEscapePressed += EscapePressed;
     

        _inputService.SwitchControlMap(GameCoreState.Gameplay);

        _assetsLibrarySO.OnOpenWindowForType?.Invoke(WindowType.HUD);

        Debug.Log("State - Gameplay");
    }

    private void EscapePressed()
    {
        _assetsLibrarySO.OnPushStateEvent?.Invoke(GameCoreState.EscMenu);
    }

    public override void ExitState() 
    {
        _assetsLibrarySO.OnEscapePressed -= EscapePressed;
    }
}