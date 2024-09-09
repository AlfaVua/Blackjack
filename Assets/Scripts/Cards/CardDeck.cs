using System;
using System.Collections.Generic;
using Cards.Hand;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
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

        public void AnimateCard(CardInstance card, CardHandBase hand, Action onComplete)
        {
            card.RenderingOrder = 999;
            var targetPosition = hand.NextCardPosition;
            card.transform.position = transform.position;
            Vector3 midPoint = new Vector3(
                Mathf.Lerp(transform.position.x, targetPosition.x, .8f),
                Mathf.Lerp(transform.position.y, targetPosition.y, .3f),
                Mathf.Lerp(transform.position.z, targetPosition.z, .5f)
            );
            Vector3[] points = { transform.position, midPoint, targetPosition };
            card.transform.DOPath(new Path(PathType.CatmullRom, points, 5, Color.red), .75f).onComplete += () =>
            {
                onComplete.Invoke();
            };
        }
    }
}