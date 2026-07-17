using UnityEngine;
using VContainer;

public class PlayerEntity : MonoBehaviour
{
    private PlayerData _playerData;

    [Inject]
    public void Construct(PlayerData playerData)
    {
        _playerData = playerData;

        _playerData._playerRuntimeData.RegisterPlayer(this);
    }

    private void OnDestroy()
    {
        if(_playerData != null ) _playerData._playerRuntimeData.UnregisterPlayer();
    }
}
