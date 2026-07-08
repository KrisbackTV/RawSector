using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;
    public Image slider;

    private void Start()
    {
        healthSystem.OnHealthChangeLocal += UpdateBar;
    }

    private void UpdateBar(float currentHealth, float maxHealth)
    {
        slider.fillAmount = currentHealth / 100;
    }
}
