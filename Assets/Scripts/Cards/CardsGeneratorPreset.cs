using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    using Utils.BoolExtension;
    [CreateAssetMenu(menuName = "Card Generator Preset")]
    public class CardsGeneratorPreset : ScriptableObject
    {
        public bool whitesEnabled = true;
        public bool blacksEnabled = true;
        public Sprite[] whites;
        public Sprite[] blacks;
        public List<RawCardData> cards;
        public List<string> cardTypes;
    
        public int TotalCards => cardTypes.Count * cards.Count * (whitesEnabled.ToInt() + blacksEnabled.ToInt());
    }
}