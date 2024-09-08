using System.Collections.Generic;
using Cards;
using UnityEngine;

public class DealerHandler : MonoBehaviour
{
    [SerializeField] private CardDeck deck;

    [SerializeField] private CardGroupBase playerCards;
    [SerializeField] private CardGroupBase dealerCards;

    private System.Random _random;

    public void Init(List<CardData> cards)
    {
        Shuffle(cards);
        deck.Init(cards);
        playerCards.Init();
        dealerCards.Init();
        AddPlayerCard();
        AddPlayerCard();
        AddDealerCard();
        AddDealerCard();
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

    private void AddPlayerCard()
    {
        GiveNextCard(playerCards);
        GameController.Instance.PlayerValueChanged(playerCards.CurrentValue);
    }

    private void AddDealerCard()
    {
        GiveNextCard(dealerCards);
    }

    private void GiveNextCard(CardGroupBase group)
    {
        group.AddCard(deck.GetNext());
    }
}