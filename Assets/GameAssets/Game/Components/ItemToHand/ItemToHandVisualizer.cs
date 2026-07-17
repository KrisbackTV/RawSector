using UnityEngine;

public class ItemToHandVisualizer : MonoBehaviour
{
    [SerializeField] private Transform _handGripPoint;

    private GameObject _currentHandObject;

    public void EquipItemVisual(ItemInstance itemData)
    {
        ClearHand();

        if (itemData == null || itemData._itemData == null || itemData._itemData.equipmentPrefab == null) return;

        _currentHandObject = Instantiate(itemData._itemData.equipmentPrefab, _handGripPoint);

        _currentHandObject.transform.localPosition = Vector3.zero;
        _currentHandObject.transform.localRotation = Quaternion.identity;
    }

    public void ClearHand()
    {
        if (_currentHandObject != null)
        {
            Destroy(_currentHandObject);
            _currentHandObject = null;
        }

        Debug.Log("Excelend)");
    }
}
