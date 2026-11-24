using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable, IEntityModule
{
    private UnitFacts _unitFacts;
    private IHealthStatsProvider _healthStatsProvider;
    private readonly List<DamageInfo> _damageLog = new List<DamageInfo>();
    public event Action<DamageInfo> OnDamageReceived;
    public event Action<DamageInfo> OnDeath;

    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null) return;

        _unitFacts = context._unitFacts;
        _healthStatsProvider = context.HealthStatsProvider;
    }



    public void TakeDamage(DamageInfo damageInfo)
    {
        if (_healthStatsProvider == null || _healthStatsProvider.CurrentHealth <= 0f) return;

        DamageInfo finalDamage = ModifyDamageThroughModules(damageInfo);

        float newHealth = _healthStatsProvider.CurrentHealth - finalDamage.RawAmount;

        _healthStatsProvider.UpdateHealth(newHealth);

        _damageLog.Add(finalDamage);
        if(_damageLog.Count > 20) _damageLog.RemoveAt(0);

        OnDamageReceived?.Invoke(finalDamage);

        if (_healthStatsProvider.CurrentHealth <= 0)
        {
            OnDeath?.Invoke(finalDamage);
        }
    }

    private DamageInfo ModifyDamageThroughModules(DamageInfo damageInfo)
    {
        //здесь надо модифицировать!!! Дописать проверку модулей
        return damageInfo;
    }

    public IReadOnlyList<DamageInfo> GetDamageLog() => _damageLog;
}
