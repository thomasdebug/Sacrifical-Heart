using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Cards
{

    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int damage;
        public int block;
        public List<DamageType> damageType;
        public Sprite cardSprite;

        public enum CardType
        {
            Attack,
            Defense,
            Skill
        }

        public enum DamageType
        {
            Physical,
            Magical,
            True,
            none
        }
    }
}
