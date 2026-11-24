using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CoreScene : MonoBehaviour
{
    [SerializeField] public bool LoadTestScene;
    [SerializeField] public AssetsLibrarySO _assetsLibrarySO;
    [SerializeField] public SceneLoader _sceneLoader;
    
    [SerializeField] public UIManager _uiManager;
    

    //Кэш
    private InputService _inputService;
    private InventoryService _inventoryService;
    private PlayerData _playerData;
    private PlayerSpawner _playerSpawner;
    private LocationCore CurrentLocationCore;


    private async void Start()
    {
        _inputService = new InputService();
        _inputService.Initialize();

        _playerData = new PlayerData(new PlayerRuntimeData(_assetsLibrarySO._playerDataSO));
        _playerSpawner = new PlayerSpawner(_assetsLibrarySO.playerPrefab, _inputService, _playerData);

        _inventoryService = new InventoryService(_inputService, _playerData._playerInventory);

        if (_uiManager != null)
            _uiManager.Initialize();






        if (LoadTestScene)
        {
            await _sceneLoader.LoadLocation("TestLocation");
        }

        GameObject locationCore = GameObject.Find("CoreLocation");
        if (locationCore != null)
        {
            CurrentLocationCore = null;

            var locationCoreScript = locationCore.GetComponent<LocationCore>();
            if (locationCoreScript != null)
            {
                CurrentLocationCore = locationCoreScript;
                GameLog.LogDebug($"Найден Core сцены");
            }
        }
        else
        {
            GameLog.LogDebug($"Core сцены НЕ найден");
            return;
        }


        if (CurrentLocationCore != null)
        {
            CurrentLocationCore.Initialize();
        }






        PlayerSpawnPoint pointForSpawn = null;
        foreach (PlayerSpawnPoint point in CurrentLocationCore._pointList)
        {
            if (point.PointName == "spawn")
            {
                pointForSpawn = point;
            }
        }

        if (pointForSpawn != null)
            _playerSpawner.RespawnPlayer(pointForSpawn);
    }
}
