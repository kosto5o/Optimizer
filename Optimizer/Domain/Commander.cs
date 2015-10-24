using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Domain
{
    public class Commander : Card
    {
        public Commander(int attack, int health, int delay) : base(0, health, 0)
        {            
        }

    }
}
