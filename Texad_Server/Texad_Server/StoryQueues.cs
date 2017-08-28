using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    
    public class StoryQueues
    {
        public static string[] dawnQueues =
        {
            "It is now dawn."
        };

        public static string[] duskQueues =
        {
            "It is now dusk."
        };

        public static string getDawnQueue()
        {
            return dawnQueues[new Random().Next(dawnQueues.Length)];
        }

        public static string getDuskQueue()
        {
            return duskQueues[new Random().Next(duskQueues.Length)];
        }
    }
}
