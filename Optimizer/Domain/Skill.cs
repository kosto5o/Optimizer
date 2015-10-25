using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Domain
{
    public class Skill
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
    }

    public enum SkillArea
    {
        Single,
        Multiple,
        All,
        AllImperial,
        AllXeno,
        AllRaider,
        AllRighteous,
        AllBloodthirsty
    }

    public enum SkillType
    {
        NoSkill,
        Siege
    }
}
