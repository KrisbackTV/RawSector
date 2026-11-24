using TMPro;
using UnityEngine;

public class ItemNameDebager : MonoBehaviour
{
    private TextMeshProUGUI _debugItemName;

    private void Start()
    {
        if (_debugItemName == null)
        {
            _debugItemName = GetComponent<TextMeshProUGUI>();
        }

        Subscribe();
    }


    private void SetDebugItemText(string itemName)
    {
        if (_debugItemName != null)
        {

            _debugItemName.text = itemName;

        }
    }


    private void Subscribe()
    {
        //_gameActions.OnInteractItemName += SetDebugItemText;
    }


    private void UnSubscribe()
    {
        //_gameActions.OnInteractItemName -= SetDebugItemText;
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }
}
