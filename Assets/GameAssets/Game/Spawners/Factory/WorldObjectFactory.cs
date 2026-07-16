using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class WorldObjectFactory : IWorldObjectFactory, IStartable
{
    private readonly IObjectResolver _resolver;
    private GameActions _gameActions;
    private SceneLoader _sceneLoader;

    [Inject]
    public WorldObjectFactory(IObjectResolver resolver, GameActions gameActions, SceneLoader sceneLoader)
    {
        _resolver = resolver;
        _gameActions = gameActions;
        _sceneLoader = sceneLoader;

        if (_gameActions != null)
        {
            _gameActions.OnDropItemInWorld += SpawnItem;
        }
    }

    public void Start() { }

    public void SpawnItem(ItemInstance item, Vector3 position, Quaternion rotation)
    {
            Debug.Log("запуск");
        if (item._itemData == null)
        {
            Debug.LogError("Фабрика: Попытка заспавнить пустой ItemData!");
        }

        if (item._itemData.worldPrefab == null)
        {
            Debug.LogError($"Фабрика: У предмета {item._itemData.itemName} отсутствует worldPrefab!");
        }

        GameObject spawnedObject = _resolver.Instantiate(item._itemData.worldPrefab, position, rotation);
        if (spawnedObject == null)
        {
            return;
        }

        Scene targetScene = SceneManager.GetSceneByName(_sceneLoader._currentMapName);

        if (targetScene.isLoaded)
        {
            SceneManager.MoveGameObjectToScene(spawnedObject, SceneManager.GetSceneByName(_sceneLoader._currentMapName));
        }

        var worldItem = spawnedObject.GetComponent<ItemWorld>();
        if (worldItem != null)
        {
            worldItem.Initialize(item);
            Debug.Log("инициализация");
        }
    }
}