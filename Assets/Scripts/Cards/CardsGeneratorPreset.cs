using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(menuName = "Card Generator Preset")]
    public class CardsGeneratorPreset : ScriptableObject
    {
        public Sprite[] whites;
        public Sprite[] blacks;
        public List<CardData> cards;
        public List<string> cardTypes;
    }
}