using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestGameScope : LifetimeScope
{
    [SerializeField] private List<PlayerSpawnPoint> _allpoints;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_allpoints);
        builder.RegisterEntryPoint<PlayerSpawner>();
    }
}
