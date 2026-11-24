using NUnit.Framework.Internal.Execution;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchItemForGround : MonoBehaviour, IEntityModule
{
    private IInputProvider _inputProvider;


    public float scanRadius = 3f;
    public LayerMask groundItemLayer;


    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null) return;

        _inputProvider = context.InputProvider;

        Subscribe();
    }


    public void ScanForItem()
    {
        List<ItemInstance> foundItems = new List<ItemInstance>();

        // 1. Сканируем сферу
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadius, groundItemLayer);

        // 2. Проходимся по найденным коллайдерам
        foreach (var hit in hitColliders)
        {
            // Пытаемся получить компонент предмета (например, WorldItem)
            if (hit.TryGetComponent<ItemWorld>(out var worldItem))
            {
                // Если у предмета есть данные, добавляем их в список
                if (worldItem._itemInstance != null)
                {
                    foundItems.Add(worldItem._itemInstance);
                }
            }
        }

        //временный Дебаг
        if (foundItems.Count > 0)
        {
            string allNames = string.Join(", ", foundItems.Select(item => item._itemName));
            GameLog.LogInfo($"найденные предметы: {allNames}");
        }
    }


    private void Subscribe()
    {
        if (_inputProvider != null)
        {
            _inputProvider.OnOpenInventory += ScanForItem; 
        }
    }
    
    private void UnSubscribe()
    {
        if (_inputProvider != null)
        {
            _inputProvider.OnOpenInventory -= ScanForItem;
        }
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
}
