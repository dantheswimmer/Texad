using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    class StatManager
    {
        public List<TexadStat> stats;

        public TexadStat getStatWithName(string name)
        {
            foreach (TexadStat s in stats)
            {
                if (s.statName.Equals(name))
                    return s;
            }
            return null;
        }
    }
}
