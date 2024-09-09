using System.Collections.Generic;
using Components;
using TMPro;
using UnityEngine;

namespace Cards.Hand
{
    public class CardHandBase : MonoBehaviour
    {
        [SerializeField] private CardHorizontalGroup container;
        [SerializeField] protected TextMeshProUGUI valueText;
        [SerializeField] private string valueInfo;

        protected List<CardInstance> _cards;

        public int CurrentValue { get; private set; }
        public Vector3 NextCardPosition => container.GetNextItemTargetPosition(true);

        private void Awake()
        {
            _cards = new List<CardInstance>();
        }

        public virtual void Init()
        {
            CurrentValue = 0;
            container.Reset();
            _cards.Clear();
        }

        public void AddCard(CardInstance card)
        {
            _cards.Add(card);
            container.AddToGroup(card);
            CurrentValue += card.GetValue(CurrentValue);
            valueText.text = valueInfo + CurrentValue;
            OnCardAdded(card);
        }

        protected virtual void OnCardAdded(CardInstance card)
        {
        }
    }
}