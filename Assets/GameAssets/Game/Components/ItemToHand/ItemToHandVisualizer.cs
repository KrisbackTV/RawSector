using UnityEngine;

public class ItemToHandVisualizer : MonoBehaviour, IEntityModule
{
    private IEquipmentDataProvider _equipmentDataProvider;

    public void OnEntityInitializer(IEntityContext context)
    {
        _equipmentDataProvider = context.EquipmentDataProvider;

        //EquipItemVisual(context.CurrentHandItem);
        Subscribe();
    }

    [SerializeField] private Transform _handGripPoint;

    private GameObject _currentItemToHand;



    public void EquipItemVisual(ItemInstance itemData)
    {
        ClearHand();

        if (itemData == null || itemData._itemData == null || itemData._itemData.equipmentPrefab == null) return;

        _currentItemToHand = Instantiate(itemData._itemData.equipmentPrefab, _handGripPoint);

        _currentItemToHand.transform.localPosition = Vector3.zero;
        _currentItemToHand.transform.localRotation = Quaternion.identity;
    }

    public void ClearHand()
    {
        if (_currentItemToHand != null)
        {
            Destroy(_currentItemToHand);
            _currentItemToHand = null;
        }
    }

    private void Subscribe()
    {
        _equipmentDataProvider.OnActiveItemChanged += EquipItemVisual;
    }

    private void UnSubscribe()
    {
        _equipmentDataProvider.OnActiveItemChanged -= EquipItemVisual;
    }

    private void OnDisable() => UnSubscribe();

    private void OnDestroy() => UnSubscribe();
}
