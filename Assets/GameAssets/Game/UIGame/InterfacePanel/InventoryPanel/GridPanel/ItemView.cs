using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemCount;

    public void SetView(Sprite sprite, int count)
    {
        if (sprite != null && _itemImage != null)
        {
            SetImage(sprite);
        }

        if (_itemCount != null)
        {
            SetCount(count);
        }
    }

    private void SetImage(Sprite itemImage)
    {
        _itemImage.sprite = itemImage;
    }

    private void SetCount(int count)
    {
        _itemCount.text = count.ToString();
    }


    //Для тестов:
    private void SetColor(Color itemColor)
    {
        _itemImage.color = itemColor;
    }
}
