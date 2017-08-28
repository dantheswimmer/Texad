using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadActor
    {
        public string actorName = "actor";
        public string actorDescription = "An actor";
        public bool hidden = false;

        public TexadActor()
        {

        }
        public TexadActor(string n, string d)
        {
            actorName = n;
            actorDescription = d;
        }

        public string getSceneDescriptionString()
        {
            return actorName + ", " + actorDescription;
        }
    }

    public class TexadCharacter : TexadActor
    {
        public TexadBehavior attackBehavior;
        public TexadBehavior defenceBehavior;
        public TexadBehavior movementBehavior;

    }

    public class TexadNPC : TexadCharacter
    {

    }

    public class TexadPlayer : TexadCharacter
    {

    }
}
