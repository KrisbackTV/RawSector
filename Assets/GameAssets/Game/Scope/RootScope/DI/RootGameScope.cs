using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootGameScope : LifetimeScope
{
    [Header("Service")]
    [SerializeField] private GameStateMachine _gameStateMachine;

    [Header("Data")]
    [SerializeField] private AssetsLibrarySO _assetsLibrarySO;

    protected override void Configure(IContainerBuilder builder)
    {
        //SERVICE
        builder.RegisterInstance(_gameStateMachine);
        builder.Register<InventoryActions>(Lifetime.Singleton);
        builder.Register<SceneLoader>(Lifetime.Singleton);
        builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<GameCore>(Lifetime.Singleton);

        //DATA
        builder.RegisterInstance(_assetsLibrarySO);

        //ПРОЧЕЕ
        builder.RegisterEntryPoint<GameStarter>();
        builder.RegisterEntryPoint<StateMachineRunner>();




        builder.RegisterBuildCallback(resolver =>
            {
                _gameStateMachine.Initialize();


                resolver.Inject(_gameStateMachine);

                foreach (var state in _assetsLibrarySO._allState)
                {
                    if (state != null)
                    {
                        resolver.Inject(state);
                    }
                }
            });
    }
}
