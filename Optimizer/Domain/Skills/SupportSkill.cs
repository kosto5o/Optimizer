using System;

namespace Optimizer.Domain.Skills
{
    public sealed class SupportSkill : Skill
    {
        public override void Attack(Deck currentPlayerDeck, Deck currentEnemyDeck, Card skillOwnerCard)
        {
            throw new NotImplementedException();
        }
    }
}
