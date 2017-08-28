using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadSerializer
    {
        public static string serializeInventory(TexadClient tc)
        {
            string serStr = "i";
            for(int i = 0; i < tc.playerItems.Count; i++)
            {
                serStr += tc.playerItems[i].serializeItemData();
                if(i < tc.playerItems.Count-1)
                    serStr += "|";
            }
            return serStr;
        }

        public static string serializeStats(TexadClient tc)
        {
            string serStr = "u";
            for(int i = 0; i < tc.playerStats.Count; i++)
            {
                serStr += tc.playerStats[i].serializeStatData();
                if (i < tc.playerStats.Count - 1)
                    serStr += "|";
            }
            return serStr;
        }

        public static string serializeCompanions(TexadClient tc)
        {
            return "";
        }
    }
}
