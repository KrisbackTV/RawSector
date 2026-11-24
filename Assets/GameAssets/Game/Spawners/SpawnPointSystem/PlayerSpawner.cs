
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{
    private GameObject PlayerPrefab { get; set; }
    private InputService InputService { get; set; }

    //Кэш
    private PlayerData _playerData;
    private GameObject _currentPlayer;

    public PlayerSpawner(GameObject playerPrefab, InputService inputService, PlayerData playerData)
    {
        _playerData = playerData;
        PlayerPrefab = playerPrefab;
        InputService = inputService;
    }

    public void RespawnPlayer(PlayerSpawnPoint point)
    {
        if (_currentPlayer != null)
        {
            Object.Destroy(_currentPlayer);

            //if (_playerData != null)
            //{
            //    _playerData._playerRuntimeData.UpdateHealth(_playerData._playerRuntimeData.MaxHealth);
            //    _playerData._playerRuntimeData.UpdateStamina(_playerData._playerRuntimeData.MaxStamina);
            //}
        }

        _currentPlayer = SpawnPlayer(PlayerPrefab, point);
    }

    private GameObject SpawnPlayer(GameObject playerPrefab, PlayerSpawnPoint point)
    {
        var targetPoint = point;
        var targetScene = point.gameObject.scene;
        var prefab = playerPrefab;

        var player = Object.Instantiate(prefab, targetPoint.transform.position, targetPoint.transform.rotation);
        PlayerBrain playerBrain = player.GetComponentInParent<PlayerBrain>();

        if (playerBrain == null) 
            GameLog.LogError("brain not searching");
        else
        {
            playerBrain.Initialize(InputService, _playerData);
        }

        return player;
    }
}
