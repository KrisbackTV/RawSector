using UnityEngine;

public class SetActiveComponent : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public void SetActive(bool value)
    {
        if (_canvasGroup == null) return;

        if (value)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = 0;
        }
    }
}
