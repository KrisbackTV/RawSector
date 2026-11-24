using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StamineBar : MonoBehaviour
{
    public IStamineStatsProvider _staminaData;
    public Image BarStamine;

    public void Construct(IStamineStatsProvider staminaData)
    {
        _staminaData = staminaData;
        _staminaData.OnStaminaChanged += Refresh;

        Refresh(_staminaData.CurrentStamina, _staminaData.MaxStamina);
    }

    private void Refresh(float currentStamina, float maxStamina)
    {
        if( maxStamina <= 0) return;

        BarStamine.fillAmount = currentStamina / maxStamina;
    }
}
