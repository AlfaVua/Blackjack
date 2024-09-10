using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class RawCardData
    {
        public int value;
        public string name;
    }
    public class CardGenerator : MonoBehaviour
    {
        [SerializeField] private CardsGeneratorPreset preset;

        private int totalCards;
        private List<CardData> cards;

        public List<CardData> CardList => cards;

        private void Awake()
        {
            cards = new List<CardData>();
        }

        public void InitCardData()
        {
            cards.Clear();
            Generate();
        }
        
        private void Generate()
        {
            cards.Clear();;
            totalCards = preset.TotalCards;
            GenerateCards();
        }

        private void GenerateCards()
        {
            foreach (var type in preset.cardTypes)
            {
                foreach (var card in preset.cards)
                {
                    if (preset.whitesEnabled) GenerateCard(card, type, true);
                    if (preset.blacksEnabled) GenerateCard(card, type, false);
                }
            }
        }
        
        private void GenerateCard(RawCardData data, string type, bool isWhite)
        {
            var cardName = type + "_" + data.name + (isWhite ? "_white" : "_black");
            cards.Add(new CardData(data.value, GetCardSprite(cardName, isWhite ? preset.whites : preset.blacks), isWhite));
            OnCardGenerated();
        }

        private Sprite GetCardSprite(string cardName, Sprite[] sprites)
        {
            return sprites.First(sprite => sprite.name == cardName);
        }

        private void OnCardGenerated()
        {
            if (cards.Count == totalCards) OnComplete();
        }

        private void OnComplete()
        {
            GameController.Instance.StartNewGame();
        }
    }
}