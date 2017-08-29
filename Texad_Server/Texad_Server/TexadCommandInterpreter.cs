using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadCommandInterpreter
    {
        public List<string> releventVerbs;
        public List<string> releventNouns;

        public static void interperateCommand(string cmd, TexadClient client)
        {
            string verb = null;
            string noun = null;
            if (cmd.Contains(" "))
            {
                int splitIndex = cmd.IndexOf(' ');
                if (splitIndex == cmd.Length || splitIndex == -1)
                {

                }
                else
                {
                    noun = cmd.Substring(splitIndex + 1);
                }
                verb = cmd.Substring(0, splitIndex);
            }
            else
            {
                verb = cmd;
            }
            Console.WriteLine("verb was: " + verb + ", noun was: " + noun);
            if (verb != null)
            {      
                foreach(TexadAction a in client.availableActions)
                {
                    if(verb.Equals(a.actionName))
                    {
                        //Get target from noun, source is probably client
                        client.addToEventQueue(new TexadActionEvent(a, noun, client));
                        //a.doAction(noun,client);
                    }
                }
            }
        }
    }
}
