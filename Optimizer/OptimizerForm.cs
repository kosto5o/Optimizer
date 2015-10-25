using Optimizer.Optimizer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Optimizer.Domain;
using Optimizer.Domain.Skills;
using Optimizer.Utils;

namespace Optimizer
{
    public partial class OptimizerForm : Form
    {
        public OptimizerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var iterationsText = txtIterations.Text;
            var iterations = 0;
            if (!Int32.TryParse(iterationsText, out iterations))
            {
                rtbOutputDetails.Text = "Invalid Iterations";
                return;
            }

            var playerCards = new Stack<Card>();
            playerCards.Push(new Card(2, 14, 1) { Name = "Zodiac" });
            playerCards.Push(new Card(3, 18, 1) { Name = "Zodiac Double" });
            playerCards.Push(new Card(0, 15, 2) { Name = "Some tower", CardType = CardType.Tower });

            Deck playerDeck = new Deck(true, playerCards);
            playerDeck.Commander = new Commander(0, 20, 0);
            


            var enemyCards = new Stack<Card>();
            enemyCards.Push(new Card(4, 20, 2) { Name = "Aegis" });
            enemyCards.Push(new Card(1, 17, 1) { Name = "Wham" });
            var towerAttackCard = new Card(1, 17, 1) {Name = "Wham Tower Attacker"};
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
            

            BasicOptimizer optimizer = new BasicOptimizer();
            var result = optimizer.RunOptimization(playerDeck, enemyDeck, cbxSurgeMode.Checked, iterations);

            rtbOutputDetails.Text =  Logger.DumpLog();
            
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.AppendFormat("Win/Loss/Draw: {0}/{1}/{2}", result.NumberOfPlayerWins,
                result.NumberOfPlayerLosses, result.NumberOfPlayerDraws);
            resultBuilder.AppendLine();
            resultBuilder.AppendFormat("Ratio Win/Loss/Draw: {0}/{1}/{2}", result.WinRatio,
                result.LossRatio, result.DrawRatio);

            rtbSimulationResult.Text = resultBuilder.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
