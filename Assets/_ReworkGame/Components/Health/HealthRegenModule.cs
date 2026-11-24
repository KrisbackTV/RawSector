using UnityEngine;

public class HealthRegenModule : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;

    private IHealthStatsProvider _healthStatsProvider;
    private HealthSystem _healthSystem;
    private float _delayTimer;

    public void OnEntityInitializer(IEntityContext context)
    {
        _healthStatsProvider = context.HealthStatsProvider;

        _healthSystem = GetComponent<HealthSystem>();
        if (_healthSystem != null)
        {
            _healthSystem.OnDamageReceived += ResetRegenDelay;
        }
    }

    private void Update()
    {
        ProcessRegen(Time.deltaTime);
    }

    public void ProcessRegen(float deltaTime)
    {
        if (_healthStatsProvider == null) return;

        if (_healthStatsProvider.CurrentHealth >= _healthStatsProvider.MaxHealth || _healthStatsProvider.CurrentHealth <= 0f)
        {
            return;
        }

        if (_delayTimer > 0f)
        {
            _delayTimer -= deltaTime;
            return;
        }

        float regenAmount = _healthStatsProvider.HealthRegenRate * deltaTime;
        float targetHealth = _healthStatsProvider.CurrentHealth + regenAmount;

        _healthStatsProvider.UpdateHealth(targetHealth);
    }

    private void ResetRegenDelay(DamageInfo damageInfo)
    {
        if (_healthStatsProvider == null) return;
        _delayTimer = _healthStatsProvider.HealthRegenDelay;
    }


    private void UnSubscriber()
    {
        if (_healthSystem != null)
        {
            _healthSystem.OnDamageReceived -= ResetRegenDelay;
        }
    }

    private void OnDisable() => UnSubscriber();
    private void OnDestroy() => UnSubscriber();
}