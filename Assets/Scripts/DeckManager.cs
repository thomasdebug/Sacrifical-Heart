using Cards;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;

    void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");

        allCards.AddRange(cards);
        HandManager hand = FindHandManagerInActiveScene();
        for (int i = 0; i < 5; i++)
        {
            DrawCard(hand);
        }

    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        return;  

        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex + 1) % allCards.Count;
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
