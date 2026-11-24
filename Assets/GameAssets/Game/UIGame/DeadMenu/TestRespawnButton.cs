using UnityEngine;
using UnityEngine.UI;

public class TestRespawnButton : MonoBehaviour
{
    [SerializeField] Button _testButton;


    private void OnEnable()
    {
        _testButton.onClick.AddListener(OnRespawnButtonClicked);
    }

    private void OnDisable()
    {
        _testButton.onClick.RemoveListener(OnRespawnButtonClicked);
    }

    private void OnRespawnButtonClicked()
    {
        //if( _gameActions != null ) _testButton.onClick.AddListener(() => _gameActions.InvokeOnRespawnPlayer());
    }
}
