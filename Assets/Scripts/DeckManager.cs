using Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 6;

    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    [SerializeField]private HandManager handManager;

    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        HandManager hand = FindHandManagerInActiveScene();
        maxHandSize = hand.maxHandSize;
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            DrawCard(hand);
        }
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
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
}
