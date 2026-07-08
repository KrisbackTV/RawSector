using TMPro;
using UnityEngine;
using VContainer;

public class ItemNameDebager : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;
    }

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
        _assetsLibrarySO.OnInteractItemName += SetDebugItemText;
    }


    private void UnSubscribe()
    {
        _assetsLibrarySO.OnInteractItemName -= SetDebugItemText;
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
