using Optimizer.Utils;

namespace Optimizer.Domain
{
    public class Commander : Card
    {
        public Commander(int attack, int health, int delay) : base(0, health, 0)
        {            
            this.CardType = CardType.Commander;            
        }

        public override Deck BasicAttack(Deck currentPlayerDeck, Deck currentEnemyDeck)
        {
            Logger.Log("No basic attack for commander.");
            return null;
        }
    }
}
