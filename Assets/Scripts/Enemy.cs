using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth = 30;

    public int attackDamage = 5;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"{gameObject.name} took {damage} damage!");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AttackPlayer(PlayerHealth player)
    {
        if (player == null || currentHealth <= 0) return;

        Debug.Log($"{gameObject.name} attacks for {attackDamage} damage!");
        player.TakeDamage(attackDamage);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} was defeated!");
        Destroy(gameObject);
    }
}