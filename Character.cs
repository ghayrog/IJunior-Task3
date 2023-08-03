using System;
using System.Collections.Generic;

namespace CardGame
{
    public class Character
    {
        public int Wallet { get; private set; }
        public List<Card> Deck { get; private set; }

        public Character() {
            Wallet = 0;
            Deck = new List<Card>();
        }

        public void AddCardToDeck(Card _card)
        { 
            Deck.Add(_card);
        }

        public void RemoveCardFromDeck(int _cardId) {
            if (_cardId < Deck.Count) Deck.RemoveAt(_cardId);
        }

        public void AddMoney(int _valueAdd)
        { 
            if (_valueAdd >= 0)
            {
                Wallet += _valueAdd;
            }
        }

        public void SpendMoney(int _valueSpent)
        {
            if (Wallet - _valueSpent >= 0)
            {
                Wallet-= _valueSpent;
            }
        }
    }
}
