using System;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IDamageable, IHealth
{
    public EnemyDataSO enemyData;

    private float currentHealth;
    private bool isDead = false;
    private bool canRegenerate = false;
    private float regenRate = 0.5f;
    private float regenDelay = 3f;


    [Header("Глобальные события (для Игрока/Босса")]
    public GameEvent OnHealChangeGlobal;
    public GameEvent OnDeadGlobal;

    //Свойства
    public event Action<float, float> OnHealthChangeLocal;
    public event Action OnDiedLocal;
    
    //public float CurrentHealth
    //{
    //    get => (enemyData is PlayerDataSO) ? enemyData.currentHealth : currentHealth;
    //    set
    //    {
    //        if (enemyData is PlayerDataSO) enemyData.currentHealth = value;
    //        else currentHealth = value;
    //    }
    //}

    public void Initialize(EnemyDataSO enemyDataSO)
    {
        if (enemyData == null) return;

        if (!(enemyData is PlayerDataSO))
        {
            currentHealth = enemyData.maxHealth;
        }

        canRegenerate = true;
    }

    private void FixedUpdate()
    {
        //if (canRegenerate && CurrentHealth < enemyData.maxHealth)
        //{
        //    Heal(regenRate);
        //}
    }


    public void TakeDamage(float damage)
    {
        //if (isDead) return;

        //CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        //NotifiChanges();

        //if (CurrentHealth <= 0)
        //{
        //    Die();
        //}
    }

    public void Heal(float amount)
    {
        //if (isDead) return;
        //CurrentHealth = Mathf.Min(enemyData.maxHealth, CurrentHealth + amount);

        //NotifiChanges();
    }

    public void Die()
    {
        isDead = true;

        OnDiedLocal?.Invoke();
        if(OnDeadGlobal !=  null) OnDeadGlobal.Raise();
    }


    private void NotifiChanges()
    {
        //OnHealthChangeLocal?.Invoke(CurrentHealth, enemyData.maxHealth);
        if (OnHealChangeGlobal != null) OnHealChangeGlobal.Raise();
    }
}
