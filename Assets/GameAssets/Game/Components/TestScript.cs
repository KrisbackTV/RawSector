using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond = 5f;
    [SerializeField] private string _sourceName = "Магма";
    [SerializeField] private DamageType _damageType = DamageType.Anomaly; // Или Anomaly, или Kinetic в зависимости от твоих SO

    // Используем OnTriggerStay, так как он вызывается каждый кадр, пока объект внутри
    private void OnTriggerStay(Collider other)
    {
        // Ищем твой универсальный интерфейс получения урона
        IDamagable damageable = other.GetComponent<IDamagable>();

        if (damageable != null)
        {
            // Рассчитываем урон на этот конкретный кадр
            float damageThisFrame = _damagePerSecond * Time.deltaTime;

            // Формируем паспорт урона
            DamageInfo damageInfo = new DamageInfo(
                damageThisFrame,
                _damageType,
                _sourceName,
                HitZone.Legs // Наступили ногами!
            );

            // Наносим урон
            damageable.TakeDamage(damageInfo);
        }
    }
}
