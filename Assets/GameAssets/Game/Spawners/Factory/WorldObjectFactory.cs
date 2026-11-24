using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldObjectFactory : IWorldObjectFactory
{
    private SceneLoader _sceneLoader;

    public WorldObjectFactory(SceneLoader sceneLoader)
    {
        if (sceneLoader == null)
        {
            string isScenLoader = sceneLoader != null ? "присутствует" : "отсутствует";

            GameLog.LogError($"Какой то из компонентов остутствует (SceneLoader:{isScenLoader})");
        }

        _sceneLoader = sceneLoader;

        //if (_gameActions != null)
        //{
        //    _gameActions.OnDropItemInWorld += SpawnItem;

        //    GameLog.LogInfo("Фабрика создания предметов в мире успешнос создана");
        //}
    }

    public void Start() { }

    public void SpawnItem(ItemInstance item, Vector3 position, Quaternion rotation)
    {
        if (item._itemData == null)
        {
            GameLog.LogError("Попытка заспавнить пустой ItemData!");
        }

        if (item._itemData.worldPrefab == null)
        {
            GameLog.LogError($"У предмета {item._itemData.itemName} отсутствует worldPrefab!");
        }

        //GameObject spawnedObject = _resolver.Instantiate(item._itemData.worldPrefab, position, rotation);
        //if (spawnedObject == null) return;

        //Scene targetScene = SceneManager.GetSceneByName(_sceneLoader._currentMapName);

        //if (targetScene.isLoaded)
        //{
        //    SceneManager.MoveGameObjectToScene(spawnedObject, SceneManager.GetSceneByName(_sceneLoader._currentMapName));
        //}

        //var worldItem = spawnedObject.GetComponent<ItemWorld>();
        //if (worldItem != null)
        //{
        //    worldItem.Initialize(item);
        //}
    }
}