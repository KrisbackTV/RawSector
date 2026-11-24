using UnityEngine;

public class TestContainer : MonoBehaviour, IInteractable
{
    public AssetsLibrarySO assetsLibrarySO;
    
    
    public InventoryData containerData;


    private void Start()
    {
        containerData = new InventoryData();

        for (int i = 0; i < 30; i++)
        {
            ItemInstance item = new ItemInstance(assetsLibrarySO.testItemData);
            containerData.TryAddItem(item);
        }
    }


    public void Interact(GameObject interactor)
    {
        EventBus.OpenContainer(containerData);
    }

    public string GetInteractText()
    {
        return ("chest");
    }
}
