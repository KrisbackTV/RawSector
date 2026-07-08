using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Merchanter : MonoBehaviour, IInteractable
{
    public List<ItemData> itemDataList = new List<ItemData>();

    public void Interact(GameObject initiator)
    {
        //SalesPanelManager.Instance.SetSaleList(itemDataList);
    }

    public string GetInteractText()
    {
        return string.Empty;
    }
}
