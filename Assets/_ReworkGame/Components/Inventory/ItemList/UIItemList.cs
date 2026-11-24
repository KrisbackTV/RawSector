using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIItemList : MonoBehaviour
{
    [SerializeField] private GameObject _platePrefab;
    [SerializeField] private Transform _childObject;
    [SerializeField] private SetActiveComponent _textEmpty;
    

    public void UpdateList(List<ItemInstance> list)
    {
        //CleanObject
        if (_childObject.childCount > 0)
        {
            for (int i = _childObject.childCount - 1; i >= 0; i--)
            {
                Transform child = _childObject.GetChild(i);
                Destroy(child.gameObject);

                _textEmpty.SetActive(true);
            }
        }

        //AddPlateToList
        if (list == null || list.Count == 0)
        {
            _textEmpty.SetActive(true);
        }
        else
        {
            foreach (ItemInstance item in list)
            {
                var itemPlate = Instantiate(_platePrefab, _childObject.transform);
            }

            _textEmpty.SetActive(false);
        }
    }
}
