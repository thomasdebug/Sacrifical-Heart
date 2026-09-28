using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Cards;
using System;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    
    public TMP_Text nameText;

    public Image cardImage;

    public TMP_Text blockText;

    public TMP_Text damageText;

    public Image[] typeImages;

    public Image damageImages;

    private Color[] cardColors =
    {
        new Color(0.38f, 0.25f, 0.25f), // Attack
        new Color(0.30f, 0.40f, 0.60f), // Defense
        Color.purple // Skill
    };

    private Color[] typeColors =
    {
        Color.red, // Attack
        Color.blue, // Defense
        Color.purple // Skill
    };


    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        cardImage.color = cardColors[(int)cardData.cardType[0]];

        damageImages.color = typeColors[(int)cardData.damageType[0]];

        nameText.text = cardData.cardName;
        blockText.text = cardData.block.ToString();
        damageText.text = cardData.damage.ToString();
        
        for (int i = 0; i <typeImages.Length; i++)
        {
            if(i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
