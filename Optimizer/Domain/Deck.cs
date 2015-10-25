using System.Collections.Generic;
using Optimizer.Utils;

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

        public Card DrawNewCard()
        {
            var newPlayerCard = this.Cards.Count > 0 ? this.Cards.Pop() : null;

            if (newPlayerCard != null)
            {
                if (newPlayerCard.CardType == CardType.Assault)
                {
                    this.PlayedCards.Add(newPlayerCard);
                }
                else if (newPlayerCard.CardType == CardType.Tower)
                {
                    this.PlayedCards.Insert(0, newPlayerCard);
                }

                Logger.Log(
                    $"{this.GetDeckName()} Card {newPlayerCard.Name} put on table (Delay: {newPlayerCard.Delay} )");
            }
            return newPlayerCard;
        }

        /// <summary>
        /// Check card health, if dead, remove from played cards collection
        /// </summary>
        /// <param name="cardToCheck"></param>
        public void EvaluateCard(Card cardToCheck)
        {
            // If enemy skillOwnerCard health drop bellow 0, remove
            if (cardToCheck.Health <= 0)
            {
                Logger.Log($"{this.GetDeckName()} card '{cardToCheck.Name}' removed");
                this.PlayedCards?.Remove(cardToCheck);
            }
        }
    }
}
