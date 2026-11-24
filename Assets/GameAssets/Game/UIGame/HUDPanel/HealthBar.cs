using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public IHealthStatsProvider _healthData;
    public Image slider;

    public void Construct(IHealthStatsProvider healthData)
    {
        _healthData = healthData;
        _healthData.OnHealthChanged += UpdateBar;

        UpdateBar(_healthData.CurrentHealth, _healthData.MaxHealth);
    }

    private void UpdateBar(float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0) return;

        slider.fillAmount = currentHealth / maxHealth;
    }
}
