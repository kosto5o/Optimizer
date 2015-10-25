using System.Linq;
using Optimizer.Utils;

namespace Optimizer.Domain.Skills
{
    public sealed class AttackSkill : Skill
    {
        public override void Attack(Deck currentPlayerDeck, Deck currentEnemyDeck, Card skillOwnerCard)
        {
            Logger.Log($"{currentPlayerDeck?.GetDeckName()} Card {skillOwnerCard?.Name} : {this.SkillType} attack ({this.Power})");

            if (this.CanAttack())
            {
                var enemyTargetCards = currentEnemyDeck?.PlayedCards.Select(c => c).Where(c => c.CardType == this.GetTargetCardType()).ToList();

                // Attack first target skillOwnerCard, if there are no target cards, skip
                if (enemyTargetCards?.Count > 0)
                {
                    // Get enemy target card
                    var enemyTargetCard = enemyTargetCards.First();

                    // Attack
                    var originalCardHealth = enemyTargetCard.Health;
                    enemyTargetCard.Health -= this.Power;

                    Logger.Log(
                        $"{currentEnemyDeck?.GetDeckName()} card '{enemyTargetCard.Name}' health: {enemyTargetCard.Health} ({originalCardHealth}-{this.Power})");

                    currentEnemyDeck.EvaluateCard(enemyTargetCard);

                    this.ResetCounter();
                }

            }
            else
            {
                this.RaiseCounter();
            }
        }        
    }
}
