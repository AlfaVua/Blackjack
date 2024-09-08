using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField] private CardInstance prefab;
        [SerializeField] private TextMeshProUGUI deckSize;
        private List<CardData> _cards;
        private int _currentCardIndex = 0;
        public void Init(List<CardData> cards)
        {
            _cards = cards;
            deckSize.text = _cards.Count.ToString();
        }

        public CardInstance GetNext()
        {
            if (_currentCardIndex == _cards.Count) return null;
            var card = _cards[_currentCardIndex++];
            deckSize.text = (_cards.Count - _currentCardIndex).ToString();
            return card.CreateInstance(prefab);
        }
    }
}