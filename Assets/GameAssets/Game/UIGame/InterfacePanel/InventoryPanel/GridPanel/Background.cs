using UnityEditorInternal;
using UnityEngine;
using VContainer;

public class Background : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;

    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;
    }

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetSize(int x, int y)
    {
        if (_rectTransform != null)
        {
            _rectTransform.sizeDelta = new Vector2(x * _assetsLibrarySO.cellSize, y * _assetsLibrarySO.cellSize);
        }
    }

    public void SetSizeForInspector(int x, int y)
    {
        if (TryGetComponent<RectTransform>(out var component));

        if (component != null)
        {
            component.sizeDelta = new Vector2(x * 64, y * 64);
        }
    }
}
