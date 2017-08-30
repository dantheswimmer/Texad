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
        public List<StringObjRelation> strObjRelations;
        public ActionManager actionManager;
        public TexadClient client;

        public TexadCommandInterpreter(TexadClient client, ActionManager am)
        {
            this.client = client;
            actionManager = am;
            releventNouns = new List<string>();
            releventVerbs = new List<string>();
        }

        public List<object> getObjFromString(string str)
        {
            List<object> retList = new List<object>();
            foreach (StringObjRelation sor in strObjRelations)
            {
                if (sor.objString.Equals(str))
                    retList.Add(sor.strObject);
            }
            return retList;
        }

        public void interperateCommand(string cmd)
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
                foreach(TexadAction a in actionManager.availableActions)
                {
                    if(verb.Equals(a.actionName))
                    {
                        //Get target from noun, source is probably client
                        Console.WriteLine("Matching verb found, action is: " + a.actionName);
                        List<object> relatedObjects = getObjFromString(noun);
                        if (relatedObjects.Count == 0) { return; }//No noun to operate on, return for now
                        if (relatedObjects.Count > 1)
                        {
                            //do something here to decide w
                        }
                        //if (actionManager.actionValid(a, noun))
                        //{

                        //}
                        client.addToEventQueue(new TexadActionEvent(a, noun, client.playerCharacter));
                        break;
                        //a.doAction(noun,client);
                    }
                }
            }
        }
    }
}
