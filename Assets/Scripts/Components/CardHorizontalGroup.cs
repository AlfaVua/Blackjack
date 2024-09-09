using Cards;

namespace Components
{
    public class CardHorizontalGroup : GroupHorizontal<CardInstance>
    {
        protected override void OnItemAdded(CardInstance item)
        {
            item.RenderingOrder = items.Count;
        }
    }
}