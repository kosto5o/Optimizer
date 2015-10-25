namespace Optimizer.Domain.Skills
{
    public abstract class Skill
    {

        public SkillType SkillType { get; set; }

        public SkillArea SkillArea { get; set; }

        public int Power { get; set; }

        public int Repeat { get; set; }

        public int RepeatCounter { get; set; }

        public bool CanAttack()
        {
            return (RepeatCounter == Repeat);
        }

        public void ResetCounter()
        {
            this.RepeatCounter = 0;
        }

        public void RaiseCounter()
        {
            this.RepeatCounter++;
        }

        public CardType GetTargetCardType()
        {
            return this.SkillType == SkillType.Siege ? CardType.Tower : CardType.Assault;
        }

        
        public abstract void Attack(Deck currentPlayerDeck, Deck currentEnemyDeck, Card skillOwnerCard);        
    }
}
