using System.Collections.Generic;

namespace Optimizer.Domain
{
    public class Deck : IResettable
    {
        public Commander Commander { get; set; }

        private Stack<Card> DefaultCards { get; set; }

        public Stack<Card> Cards { get; set; }

        public List<Card> PlayedCards { get; set; }

        public bool IsPlayer { get; set; }

        public Deck(bool isPlayer, Stack<Card> cards)
        {
            this.DefaultCards = new Stack<Card>(cards);
            this.Cards = cards;

            this.PlayedCards = new List<Card>();
            this.IsPlayer = isPlayer;
        }

        public string GetDeckName()
        {
            return IsPlayer ? "Player" : "Enemy";
        }

        public void ResetToDefault()
        {
            foreach (var card in this.DefaultCards)
            {
                card.ResetToDefault();
            }
            this.Cards = new Stack<Card>(DefaultCards);

            this.Commander.ResetToDefault();
            this.PlayedCards = new List<Card>();
        }
    }
}
