using System;
using System.Collections.Generic;
using Cards;
using Cards.Hand;
using UnityEngine;

namespace Dealer
{
    public class DealerHandler : MonoBehaviour
    {
        [SerializeField] private CardDeck deck;

        private System.Random _random;
        private PlayerHand _playerCards;
        private DealerHand _dealerCards;

        public void Init(List<CardData> cards, PlayerHand playerHand, DealerHand dealerHand)
        {
            Shuffle(cards);
            deck.Init(cards);

            _playerCards = playerHand;
            _playerCards.Init();
            _dealerCards = dealerHand;
            _dealerCards.Init();
            InitStartingCards();
        }

        private void InitStartingCards()
        {
            AddPlayerCard(false);
            AddPlayerCard(false);
            AddDealerCard(false);
            AddDealerCard(false);
        }

        private void Shuffle(List<CardData> cards)
        {
            _random = new System.Random();
            var n = cards.Count;
            while (n > 1)
            {
                n--;
                var k = _random.Next(n + 1);
                (cards[k], cards[n]) = (cards[n], cards[k]);
            }
        }

        public void ShowCards()
        {
            _dealerCards.OpenHiddenCard();
        }

        public void OnPlayerStand()
        {
            ShowCards();
            var difference = CompareHands();
            if (difference < 0)
            {
                GameController.Instance.PlayerLost();
                return;
            }
            var dealerBehavior = new DealerBehavior(this, OnDealerDone, _playerCards.CurrentValue);
            dealerBehavior.TakeCards(deck, _dealerCards);
        }

        private int CompareHands()
        {
            return _playerCards.CurrentValue - _dealerCards.CurrentValue;
        }

        private void OnDealerDone()
        {
            var difference = CompareHands();
            if (_dealerCards.CurrentValue > 21 || difference > 0 ) GameController.Instance.PlayerWon();
            else if (difference == 0) GameController.Instance.GameTie();
            else GameController.Instance.PlayerLost();
        }

        public void AddPlayerCard()
        {
            AddPlayerCard(true);
        }

        private void AddPlayerCard(bool animate)
        {
            GiveNextCard(_playerCards, animate);
        }

        private void AddDealerCard(bool animate)
        {
            GiveNextCard(_dealerCards, animate);
        }

        private void GiveNextCard(CardHandBase hand, bool animate)
        {
            if (animate) AnimateNextCard(_playerCards);
            else hand.AddCard(deck.GetNext());
        }

        private void AnimateNextCard(CardHandBase hand)
        {
            var card = deck.GetNext();
            AnimateCard(card, hand);
        }

        public void AnimateCard(CardInstance card, CardHandBase hand, Action callback = null)
        {
            deck.AnimateCard(card, hand, () =>
            {
                hand.AddCard(card);
                callback?.Invoke();
            });
        }
    }
}