using System.Collections.Generic;
using Components;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class CardGroupBase : MonoBehaviour
    {
        [SerializeField] private CardHorizontalGroup container;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private string valueInfo;

        private List<CardInstance> _cards;

        public int CurrentValue { get; private set; }

        private void Awake()
        {
            _cards = new List<CardInstance>();
        }

        public void Init()
        {
            CurrentValue = 0;
        }

        public void AddCard(CardInstance card)
        {
            _cards.Add(card);
            container.AddToGroup(card);
            CurrentValue += card.GetValue(CurrentValue);
            valueText.text = valueInfo + CurrentValue;
        }
    }
}