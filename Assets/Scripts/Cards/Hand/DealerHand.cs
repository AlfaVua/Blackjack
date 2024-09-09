namespace Cards.Hand
{
    public class DealerHand : CardHandBase
    {
        private CardInstance hiddenCard;
        public override void Init()
        {
            base.Init();
            valueText.gameObject.SetActive(false);
        }
        
        protected override void OnCardAdded(CardInstance card)
        {
            if (_cards.Count != 1) return;
            card.Hidden = _cards.Count == 1;
            hiddenCard = card;
        }

        public void OpenHiddenCard()
        {
            hiddenCard.Hidden = false;
            valueText.gameObject.SetActive(true);
        }
    }
}