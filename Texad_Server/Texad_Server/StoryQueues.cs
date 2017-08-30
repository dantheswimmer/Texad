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
            "It is now dawn.",
            "A new day begins in earnest. The sun has reclaimed its throne.",
            "The sun creeps over the distant hills, bathing the land in fresh light."
        };

        public static string[] duskQueues =
        {
            "It is now dusk.",
            "Shadows overtake the twilight, and the creatures of the night begin to stir.",
            "The day ends and the red sun bleeds into the distance. The night brings its greetings." 
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
