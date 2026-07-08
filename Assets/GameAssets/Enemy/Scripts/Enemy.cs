using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public HealthSystem healthSystem;


    public virtual void TakeDamage(float damage) => healthSystem.TakeDamage(damage);

    public virtual void Attack(float damage)
    {
        TakeDamage(damage);
    }

    public virtual void Die() => healthSystem.Die();
}
