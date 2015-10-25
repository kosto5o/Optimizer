using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimizer.Domain;
using Optimizer.Domain.Skills;

namespace Optimizer.Test.Domain.Skills
{
    [TestClass]
    public class AttackSkillTest
    {
        [TestMethod]
        public void AttackTest()
        {
            var playerCards = new Stack<Card>();
            var playerTowerCard = new Card(0, 14, 0) {Name = "Zodiac", CardType = CardType.Tower};
            playerCards.Push(playerTowerCard);            
            Deck playerDeck = new Deck(true, playerCards);
            playerDeck.Commander = new Commander(0, 20, 0);



            var enemyCards = new Stack<Card>();            
            var towerAttackCard = new Card(1, 17, 0) { Name = "Wham Tower Attacker" };
            towerAttackCard.Skills.Add(new AttackSkill()
            {
                Power = 2,
                Repeat = 0,
                SkillArea = SkillArea.Single,
                SkillType = SkillType.Siege
            });
            enemyCards.Push(towerAttackCard);

            Deck enemyDeck = new Deck(false, enemyCards);
            enemyDeck.Commander = new Commander(0, 20, 0);


            playerDeck.DrawNewCard();
            enemyDeck.DrawNewCard();


            towerAttackCard.Skills.First().Attack(enemyDeck, playerDeck, towerAttackCard);

            Assert.AreEqual(12, playerTowerCard.Health);
        }

        [TestMethod]
        public void AttackTest2()
        {
            var attackSkill = new AttackSkill();

            attackSkill.Attack(null, null, null);            
        }
    }
}
