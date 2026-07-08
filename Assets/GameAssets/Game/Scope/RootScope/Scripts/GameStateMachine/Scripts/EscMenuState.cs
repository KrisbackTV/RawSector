using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "EscMenuState", menuName = "Architecture/States/EscMenuState")]
public class EscMenuState : GameState
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InputService _inputService;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InputService inputService)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputService = inputService;
    }

    public override void EnterState()
    {
        _assetsLibrarySO.OnEscapePressed += EscapePressed;


        _inputService.SwitchControlMap(GameCoreState.EscMenu);

        _assetsLibrarySO.OnOpenWindowForType?.Invoke(WindowType.EscMenu);

        Debug.Log("State - EscMenu");
    }

    private void EscapePressed()
    {
        _assetsLibrarySO.OnPopStateEvent?.Invoke();
    }

    public override void ExitState()
    {
        _assetsLibrarySO.OnEscapePressed -= EscapePressed;
    }
}
