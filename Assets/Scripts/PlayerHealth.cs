
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public int currentBlock = 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void AddBlock(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
            return;

        currentBlock += amount;

        Debug.Log("Player gained " + amount + " Block!");
        Debug.Log("Current Block: " + currentBlock);
    }


    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
            return;

        // Calculate how much damage Block absorbs.
        int blockedDamage = Mathf.Min(currentBlock, damage);
        currentBlock -= blockedDamage;

        // Calculate the damage that gets through to health.
        int remainingDamage = damage - blockedDamage;

        currentHealth -= remainingDamage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Blocked damage: " + blockedDamage);
        Debug.Log("Health damage: " + remainingDamage);

        CombatVFX vfx = FindFirstObjectByType<CombatVFX>();

        if (vfx != null)
        {
            // Play the Block impact effect if Block absorbed damage.
            if (blockedDamage > 0)
            {
                vfx.PlayEffect(
                    vfx.blockImpactEffect,
                    transform.position
                );
            }

            // Play the normal impact effect if health was damaged.
            if (remainingDamage > 0)
            {
                vfx.PlayEffect(
                    vfx.playerImpactEffect,
                    transform.position
                );
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void ResetBlock()
    {
        currentBlock = 0;
        Debug.Log("Player's Block has been reset!");
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player has been defeated!");
        CombatVFX vfx = FindFirstObjectByType<CombatVFX>();

        if (vfx != null)
        {
            vfx.PlayEffect(
                vfx.playerDeathEffect,
                transform.position
            );
        }

    }
}
