using UnityEngine;
using Cards;

public class CardCombat : MonoBehaviour
{
    public void PlayCard(Card card, Enemy target)
    {
        if (card == null || target == null)
            return;

        // Only attack cards deal damage.
        if (card.cardType.Contains(Card.CardType.Attack))
        {
            target.TakeDamage(card.damage);
        }
    }
}
