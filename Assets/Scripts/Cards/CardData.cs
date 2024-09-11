using UnityEngine;
using Object = UnityEngine.Object;

namespace Cards
{
    public class CardData
    {
        public readonly int Value;
        public readonly Sprite View;
        public readonly bool IsWhite;
        public readonly int Id;

        public CardData(int value, Sprite view, bool isWhite, int id)
        {
            Value = value;
            View = view;
            IsWhite = isWhite;
            Id = id;
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