using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Domain
{
    public class SimulationResult
    {
        public int NumberOfIterations { get; set; }

        public int NumberOfPlayerWins { get; set; }
        public int NumberOfPlayerDraws { get; set; }
        public int NumberOfPlayerLosses { get; set; }

        public double WinRatio
        {
            get { return Math.Round((double) ((double)NumberOfPlayerWins/NumberOfIterations*100), 2); }
        }

        public double LossRatio
        {
            get { return Math.Round((double) ((double)NumberOfPlayerLosses/NumberOfIterations*100), 2); }
        }

        public double DrawRatio
        {
            get { return Math.Round((double) ((double)NumberOfPlayerDraws/NumberOfIterations*100), 2); }
        }

    }
}
