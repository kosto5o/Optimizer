using System.Collections.Generic;
using System.Linq;
using Optimizer.Domain.Skills;
using Optimizer.Utils;

namespace Optimizer.Domain
{
    /// <summary>
    /// Single card
    /// </summary>
    public class Card : IResettable
    {
        public int DefaultAttack { get; set; }

        public int DefaultHealth { get; set; }

        public int DefaultDelay { get; set; }


        public int Attack { get; set; }

        public int Health { get; set; }

        public int Delay { get; set; }

        public Rarity Rarity { get; set; }

        public Faction Faction { get; set; }

        public string Name { get; set; }

        public CardType CardType { get; set; }

        public List<Skill> Skills { get; set; }

        public Card(int attack, int health, int delay)
        {
            this.DefaultAttack = attack;
            this.Attack = attack;

            this.DefaultHealth = health;
            this.Health = health;

            this.DefaultDelay = delay;
            this.Delay = delay;

            this.CardType = CardType.Assault;
            this.Skills = new List<Skill>();
        }


        public bool CanAttack()
        {
            return (this.Delay <= 0);
        }


        public void ResetToDefault()
        {
            this.Attack = this.DefaultAttack;
            this.Health = this.DefaultHealth;
            this.Delay = this.DefaultDelay;
        }

        public virtual Deck BasicAttack(Deck currentPlayerDeck, Deck currentEnemyDeck)
        {
            Logger.Log($"{currentPlayerDeck.GetDeckName()} Card {this.Name} : Basic attack ({this.Attack})");

            // Attack commander if no enemy cards on board
            var playedAssaultCards = currentEnemyDeck.PlayedCards.Select(c => c).Where(c => c.CardType == CardType.Assault).ToList();

            if (playedAssaultCards.Count == 0)
            {
                var originalHealth = currentEnemyDeck.Commander.Health;
                currentEnemyDeck.Commander.Health -= this.Attack;
                Logger.Log(
                    $"{currentEnemyDeck.GetDeckName()} Commander health: {currentEnemyDeck.Commander.Health} ({originalHealth}-{this.Attack})");


                // If Commander health is bellow 0, game over
                if (currentEnemyDeck.Commander.Health <= 0)
                {
                    Logger.Log($"{currentPlayerDeck.GetDeckName()} Win");
                    {
                        return currentPlayerDeck;
                    }
                }
            }
            else
            {
                Card enemyCard = currentEnemyDeck.PlayedCards.First(c => c.CardType == CardType.Assault);

                var originalCardHealth = enemyCard.Health;
                enemyCard.Health -= this.Attack;

                Logger.Log(
                    $"{currentEnemyDeck.GetDeckName()} card '{enemyCard.Name}' health: {enemyCard.Health} ({originalCardHealth}-{this.Attack})");

                currentEnemyDeck.EvaluateCard(enemyCard);                
            }
            return null;
        }
    }
}
