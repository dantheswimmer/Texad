using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class StatManager
    {
        public List<TexadStat> stats;

        public void addStat(TexadStat stat)
        {
            stats.Add(stat);
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
