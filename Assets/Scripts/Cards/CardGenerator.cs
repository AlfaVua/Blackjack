using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components;
using Unity.VisualScripting;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class CardData
    {
        public int value;
        public string name;
    }

    public class CardGenerator : MonoBehaviour
    {
        [SerializeField] private CardsGeneratorPreset preset;
        [SerializeField] private ProgressBar loadProgress;

        private int totalCards;

        private List<CardObject> cards;

        private void Start()
        {
            cards = new List<CardObject>();
            Generate();
        }

        private void Generate()
        {
            cards.Clear();;
            totalCards = preset.cardTypes.Count * preset.cards.Count * 2;
            loadProgress.SetValue(0);
            StartCoroutine(nameof(GenerateCards));
        }

        private IEnumerator GenerateCards()
        {
            for (int t = 0; t < preset.cardTypes.Count; t++)
            {
                for (int i = 0; i < preset.cards.Count; i++)
                {
                    GenerateCard(preset.cards[i], preset.cardTypes[t], false);
                    GenerateCard(preset.cards[i], preset.cardTypes[t], true);
                    yield return new WaitForNextFrameUnit();
                }
            }
        }
        
        private void GenerateCard(CardData data, string type, bool isWhite)
        {
            var cardName = type + "_" + data.name + (isWhite ? "_white" : "_black");
            cards.Add(new CardObject(data.value, GetCardSprite(cardName, isWhite ? preset.whites : preset.blacks)));
            OnCardGenerated();
        }

        private Sprite GetCardSprite(string cardName, Sprite[] sprites)
        {
            return sprites.First(sprite => sprite.name == cardName);
        }

        private void OnCardGenerated()
        {
            loadProgress.SetValue((float)cards.Count / totalCards);
            if (cards.Count == totalCards) OnComplete();
        }

        private void OnComplete()
        {
        }
    }
}