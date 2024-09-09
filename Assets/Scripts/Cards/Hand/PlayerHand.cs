namespace Cards.Hand
{
    public class PlayerHand : CardHandBase
    {
        protected override void OnCardAdded(CardInstance card)
        {
            base.OnCardAdded(card);
            GameController.Instance.PlayerValueChanged(CurrentValue);
        }
    }
}