using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryProvider
{
    InventoryData Inventory { get; }
    event Action<List<ItemInstance>> OnChanged;
}
