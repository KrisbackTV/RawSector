using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerStats>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        Debug.Log("PlayerStats зарегестрирован");
        
        builder.Register<PlayerData>(Lifetime.Singleton);
        Debug.Log("PlayerData зарегестрирован");
        
        builder.RegisterEntryPoint<InventoryService>(Lifetime.Singleton).AsSelf();
        Debug.Log("InventoryService зарегестрирован");
        
        builder.RegisterEntryPoint<DragAndDropService>(Lifetime.Singleton).AsSelf();
        Debug.Log("DragAndDropService зарегестрирован");
        
        builder.Register<EquipmentService>(Lifetime.Singleton);
        Debug.Log("EquipmentService зарегестрирован");
    }
}
