using UnityEngine;

public class DamageBlock : Enemy
{
    [SerializeField] private float damage = 10f;

    public override void TakeDamage(float damage)
    {
    }
    public override void Attack(float damage)
    {

    }

    public override void Die()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        healthSystem = other.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Не получилось");
        }
    }
}
