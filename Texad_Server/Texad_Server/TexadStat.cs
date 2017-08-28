using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadStat
    {
        public int statID;
        public string statName;
        public int currentValue;
        public int maxValue;

        public TexadStat(string n,int id, int cv = -1, int mv = -1)
        {
            statName = n;
            statID = id;
            currentValue = cv;
            maxValue = mv;
        }

        public void addToStat(int delta)
        {
            if (currentValue == -1)
                return;
            if ( maxValue != -1 && currentValue + delta > maxValue)
                currentValue = maxValue;
            else if ( currentValue + delta < 0)
                currentValue = 0;
            else
                currentValue = currentValue + delta;
        }

        public string serializeStatData()
        {
            string ret = "";
            ret += statName + ",";
            if (currentValue > -1)
                ret += currentValue;
            if (maxValue > -1)
                ret += "/" + maxValue;
            return ret;
        }
    }
}
