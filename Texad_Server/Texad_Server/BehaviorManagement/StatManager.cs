using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class StatManager
    {
        public TexadCharacter owner;
        public List<TexadStat> stats;

        public StatManager(TexadCharacter owner)
        {
            this.owner = owner;
        }

        public void addStat(TexadCharacter owner, TexadStat stat)
        {
            stats.Add(stat);
        }

        public void addBaseStats()
        {
            TexadStat hpStat = new TexadStat("Health", 0, 100, 100);
            TexadStat hungerStat = new TexadStat("Hunger", 1, 50, 100);
            TexadStat levelStat = new TexadStat("Level", 2, 1);
            TexadStat strengthStat = new TexadStat("Strength", 3, 15);
            TexadStat intelStat = new TexadStat("Intellegence", 4, 15);
            addStat(owner, hpStat);
            addStat(owner, hungerStat);
            addStat(owner, levelStat);
            addStat(owner, strengthStat);
            addStat(owner, intelStat);
        }


        public TexadStat getStatWithName(string name)
        {
            foreach (TexadStat s in stats)
            {
                if (s.statName.Equals(name))
                    return s;
            }
            return null;
        }


        public string serializeStats(StatManager sm)
        {
            string serStr = "u";
            for (int i = 0; i < stats.Count; i++)
            {
                serStr += stats[i].serializeStatData();
                if (i < stats.Count - 1)
                    serStr += "|";
            }
            return serStr;
        }
    }
}
