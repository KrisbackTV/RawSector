using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class StartGameButton : MonoBehaviour
{
    [Inject] private GameCore _gameCore;

    public void OnStartButtonClick()
    {
        _gameCore.StartNewGame();
    }
}
