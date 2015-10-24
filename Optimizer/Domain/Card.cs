using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

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


        public Card(int attack, int health, int delay)
        {
            this.DefaultAttack = attack;
            this.Attack = attack;

            this.DefaultHealth = health;
            this.Health = health;

            this.DefaultDelay = delay;
            this.Delay = delay;
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
    }
}
