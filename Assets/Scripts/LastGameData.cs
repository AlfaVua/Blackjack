using System.Collections.Generic;
using Cards;
using Cards.Hand;
using Dealer;
using UnityEngine;

public class LastGameData : ScriptableObject
{
    private List<int> playerCards;
    private List<int> dealerCards;

    public bool HaveData => playerCards != null && playerCards.Count != 0;

    public void Save(List<int> playerCards, List<int> dealerCards)
    {
        this.playerCards = playerCards;
        this.dealerCards = dealerCards;
    }

    public void Load(DealerHandler dealer)
    {
        playerCards.ForEach(dealer.LoadPlayerCard);
        dealerCards.ForEach(dealer.LoadDealerCard);
    }

    public void Clear()
    {
        playerCards = null;
        dealerCards = null;
    }
}