using Components;
using UnityEngine;

namespace Cards
{
    public class CardInstance : MonoBehaviour, IHaveBounds
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private SpriteRenderer shadow;

        private CardData _data;
        
        public int RenderingOrder
        {
            set => sprite.sortingOrder = shadow.sortingOrder = value;
        }

        public bool Hidden
        {
            set => sprite.sprite = value ? GameController.GetCardBack(_data.IsWhite) : _data.View;
        }

        public void Init(CardData cardData)
        {
            _data = cardData;
            sprite.sprite = cardData.View;
        }

        public int GetValue(int previousValue = 0)
        {
            return _data.IsAce && previousValue > 10 ? 1 : _data.Value;
        }

        public Bounds? GetBounds() // sprite bounds negative, for some reason :/
        {
            return sprite.bounds;
        }
    }
}