using UnityEngine;
using Object = UnityEngine.Object;

namespace Cards
{
    public class CardData
    {
        public int Value { get; private set; }
        public Sprite View { get; private set; }
        public bool IsWhite { get; private set; }

        public CardData(int value, Sprite view, bool isWhite)
        {
            Value = value;
            View = view;
            IsWhite = isWhite;
        }

        public bool IsAce => Value == 11;

        public CardInstance CreateInstance(CardInstance prefab)
        {
            var instance = Object.Instantiate(prefab);
            instance.Init(this);
            return instance;
        }
    }
}