using UnityEngine;

namespace Cards
{
    public class CardObject
    {
        private int _cardValue;
        private Sprite _view;

        public CardObject(int value, Sprite view)
        {
            _cardValue = value;
            _view = view;
        }

        public int Value => _cardValue;
        public Sprite View => _view;
    }
}