using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth = 30;
    public EnemyIntentUI intentUI;
    public int attackDamage = 5;
    private EnemyManager enemyManager;
    private bool isDead = false;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
        enemyManager.RegisterEnemy();
        UpdateIntentUI();
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
    public void UpdateIntentUI()
    {
        intentUI.UpdateIntent(attackDamage);
    }
    public void AttackPlayer(PlayerHealth player)
    {
        if (player == null || currentHealth <= 0) return;

        Debug.Log($"{gameObject.name} attacks for {attackDamage} damage!");
        player.TakeDamage(attackDamage);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        enemyManager.EnemyDefeated();

        Destroy(gameObject);
    }
}