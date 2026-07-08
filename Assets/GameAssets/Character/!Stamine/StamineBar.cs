using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StamineBar : MonoBehaviour
{
    public Image BarStamine;
    public TextMeshProUGUI valueStamine;

    private float maxStamine;
    private float currentStamine;

    public void SetMaxStamine(float value)
    {
        maxStamine = value;
        Refresh();
    }

    public void SetCurrentStamine(float value)
    {
        currentStamine = value;
        Refresh();
    }

    private void Refresh()
    {
        if(maxStamine <= 0) return;

        float ratio = currentStamine / maxStamine;

        if(BarStamine != null) BarStamine.fillAmount = Mathf.Clamp01(ratio);
        if (valueStamine != null) valueStamine.text = ($"{(int)currentStamine}%");
    }

}
