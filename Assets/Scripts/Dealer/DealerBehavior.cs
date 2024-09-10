using System;
using Cards;
using Cards.Hand;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dealer
{
    public class DealerBehavior
    {
        private DealerHandler _handler;
        private Action _onComplete;
        private int _playerValue;
        
        public DealerBehavior(DealerHandler handler, Action onComplete, int playerHandValue)
        {
            _handler = handler;
            _onComplete = onComplete;
            _playerValue = playerHandValue;
        }

        public void TakeCards(CardDeck deck, DealerHand hand)
        {
            TryTakeNextCard(deck, hand);
        }

        private void TryTakeNextCard(CardDeck deck, DealerHand hand)
        {
            if (hand.CurrentValue > 20 || hand.CurrentValue > _playerValue || !ShouldGetNext(hand))
            {
                _onComplete();
                return;
            }
            _handler.AnimateCard(deck.GetNext(), hand, () => TryTakeNextCard(deck, hand));
        }

        private bool ShouldGetNext(DealerHand hand)
        {
            return hand.CurrentValue < 11 || Random.value < 1 - Mathf.Pow((hand.CurrentValue - 10) / 11f, 1.5f);
        }
    }
}