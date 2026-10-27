using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class CardCombat : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public List<Enemy> enemies = new List<Enemy>();

    public bool isPlayerTurn = true;
    public float timeBetweenAttacks = 0.5f;
    public CombatVFX combatVFX;
    public DeckManager deckManager;

    public void PlayCard(Card card, Enemy target)
    {
        if (!isPlayerTurn || card == null)
            return;

        // Attack cards need an enemy target.
        if (card.cardType.Contains(Card.CardType.Attack))
        {
            if (target == null)
                return;

            target.TakeDamage(card.damage);

            if (combatVFX != null)
            {
                Debug.LogWarning("CombatVFX reference is missing!");
                combatVFX.PlayEffect(
                    combatVFX.impactEffect,
                    target.transform.position
                );
            }
            else
            {
                Debug.LogWarning("CombatVFX reference is missing!");
            }
        }

        // Defense cards do not need an enemy target.
        if (card.cardType.Contains(Card.CardType.Defense))
        {
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth is not assigned in CardCombat!");
                return;
            }

            playerHealth.AddBlock(card.block);
            
            if (combatVFX != null)
            {
                combatVFX.PlayEffect(
                    combatVFX.blockEffect,
                    playerHealth.transform.position
                );
            }
            Debug.Log("Played defense card: " + card.cardName);
        }
    }


    public void EndPlayerTurn()
    {
        if (!isPlayerTurn)
            return;

        isPlayerTurn = false;

        // Discard all cards in the player's hand.
        if (deckManager != null)
        {
            deckManager.DiscardHand();
        }

        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started!");

        foreach (Enemy enemy in enemies)
        {
            if (playerHealth == null ||
                playerHealth.currentHealth <= 0)
            {
                Debug.Log("Combat ended!");
                yield break;
            }

            if (enemy == null)
                continue;

            enemy.AttackPlayer(playerHealth);

            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        if (playerHealth != null &&
            playerHealth.currentHealth > 0)
        {
            // Remove any Block left over after enemy attacks.
            playerHealth.ResetBlock();

            // Draw a new hand for the next turn.
            if (deckManager != null)
            {
                deckManager.DrawCards(deckManager.cardsPerTurn);
            }

            isPlayerTurn = true;
            Debug.Log("Player turn started!");
        }
    }
}

