using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerRuntimeData>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        Debug.Log("PlayerStats зарегестрирован");
        
        builder.Register<PlayerData>(Lifetime.Singleton);
        Debug.Log("PlayerData зарегестрирован");
        
        builder.RegisterEntryPoint<InventoryService>(Lifetime.Singleton).AsSelf();
        Debug.Log("InventoryService зарегестрирован");
        
        builder.RegisterEntryPoint<DragAndDropService>(Lifetime.Singleton).AsSelf();
        Debug.Log("DragAndDropService зарегестрирован");
        
        builder.Register<EquipmentService>(Lifetime.Singleton);
        Debug.Log("EquipmentService зарегестрирован");

        builder.RegisterEntryPoint<WorldObjectFactory>(Lifetime.Singleton).As<IWorldObjectFactory>().AsSelf();
        Debug.Log("WorldObjectFactory зарегестрирован");
    }
}
