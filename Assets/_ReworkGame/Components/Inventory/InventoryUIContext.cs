using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIContext
{
    public InventoryData _playerData;
    public InventoryData _containerData;

    public InventoryUIContext(InventoryData playerData, InventoryData containerData)
    {
        if(playerData != null)
            _playerData = playerData;

        if(containerData != null)
            _containerData = containerData;
    }
}
