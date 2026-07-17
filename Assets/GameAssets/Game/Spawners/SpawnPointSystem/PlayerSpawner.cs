
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class PlayerSpawner : IStartable
{
    private readonly IObjectResolver _resolver;
    private readonly AssetsLibrarySO _assetsLibrarySO;
    private readonly List<PlayerSpawnPoint> _points;

    public PlayerSpawner(IObjectResolver resolver, AssetsLibrarySO assetsLibrarySO, List<PlayerSpawnPoint> points)
    {
        _resolver = resolver;
        _assetsLibrarySO = assetsLibrarySO;
        _points = points;
    }

    public void Start()
    {
        var targetPoint = _points[0];
        var targetScene = _points[0].gameObject.scene;

        var prefab = _assetsLibrarySO.playerPrefab;
        var player = _resolver.Instantiate(prefab, targetPoint.transform.position, targetPoint.transform.rotation);
        SceneManager.MoveGameObjectToScene(player, targetScene);

        //_resolver.InjectGameObject(player);
    }
}
