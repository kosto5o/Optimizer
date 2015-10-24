using Optimizer.Optimizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Optimizer.Domain;
using Optimizer.Utils;

namespace Optimizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var iterationsText = txtIterations.Text;
            var iterations = 0;
            if (!Int32.TryParse(iterationsText, out iterations))
            {
                textBox1.Text = "Invalid Iterations";
                return;
            }

            var playerCards = new Stack<Card>();
            playerCards.Push(new Card(2, 14, 1) { Name = "Zodiac" });
            playerCards.Push(new Card(3, 18, 1) { Name = "Zodiac Double" });

            Deck playerDeck = new Deck(true, playerCards);
            playerDeck.Commander = new Commander(0, 20, 0);
            


            var enemyCards = new Stack<Card>();
            enemyCards.Push(new Card(4, 20, 2) { Name = "Aegis" });
            enemyCards.Push(new Card(1, 17, 1) { Name = "Wham" });

            Deck enemyDeck = new Deck(false, enemyCards);
            enemyDeck.Commander = new Commander(0, 20, 0);
            

            BasicOptimizer optimizer = new BasicOptimizer();
            var result = optimizer.RunOptimization(playerDeck, enemyDeck, false, iterations);

            textBox1.Text =  Logger.DumpLog();
            
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.AppendFormat("WinRatio: {0}", result.WinRatio);
            resultBuilder.AppendLine();
            resultBuilder.AppendFormat("LossRatio: {0}", result.LossRatio);
            resultBuilder.AppendLine();
            resultBuilder.AppendFormat("DrawRatio: {0}", result.DrawRatio);

            richTextBox1.Text = resultBuilder.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
