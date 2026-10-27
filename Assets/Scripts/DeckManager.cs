using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public int startingHandSize = 5;
    public int cardsPerTurn = 5;

    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    [SerializeField] private HandManager handManager;

    private void Start()
    {
        if (handManager == null)
            handManager = FindFirstObjectByType<HandManager>();

        if (handManager == null)
        {
            Debug.LogError("HandManager not found!");
            return;
        }

        HandManager hand = FindHandManagerInActiveScene();
        Card[] cards = Resources.LoadAll<Card>("Cards");
        drawPile.AddRange(cards);

        ShuffleDrawPile();

        DrawCards(startingHandSize);
    }

    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Refill the Draw Pile if needed.
            if (drawPile.Count == 0)
            {
                ShuffleDiscardIntoDrawPile();
            }

            // Stop if there are no cards left anywhere.
            if (drawPile.Count == 0)
            {
                Debug.Log("No cards left to draw!");
                return;
            }

            Card card = drawPile[0];
            drawPile.RemoveAt(0);

            handManager.AddCardToHand(card);
        }

        Debug.Log("Draw Pile: " + drawPile.Count);
        Debug.Log("Discard Pile: " + discardPile.Count);
    }


    private HandManager FindHandManagerInActiveScene()
    {
        var scene = SceneManager.GetActiveScene();
        if (!scene.isLoaded) return null;

        foreach (var root in scene.GetRootGameObjects())
        {
            // includeInactive: true to match behavior that might have found disabled objects
            var hm = root.GetComponentInChildren<HandManager>(true);
            if (hm != null) return hm;
        }

        return null;
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
    }


    public void DiscardHand()
    {
        // Copy the list so we can safely remove cards.
        List<GameObject> cardsToDiscard =
            new List<GameObject>(handManager.cardsInHand);

        foreach (GameObject cardObject in cardsToDiscard)
        {
            if (cardObject == null)
                continue;

            CardDisplay display = cardObject.GetComponent<CardDisplay>();

            if (display != null && display.cardData != null)
            {
                discardPile.Add(display.cardData);
            }

            Destroy(cardObject);
        }

        handManager.cardsInHand.Clear();

        Debug.Log("Hand discarded!");
        Debug.Log("Discard Pile: " + discardPile.Count);
    }

    private void ShuffleDiscardIntoDrawPile()
    {
        if (discardPile.Count == 0)
            return;

        drawPile.AddRange(discardPile);
        discardPile.Clear();

        ShuffleDrawPile();

        Debug.Log("Discard Pile shuffled into Draw Pile!");
    }

    private void ShuffleDrawPile()
    {
        for (int i = 0; i < drawPile.Count; i++)
        {
            int randomIndex = Random.Range(i, drawPile.Count);

            Card temp = drawPile[i];
            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
    }
}

