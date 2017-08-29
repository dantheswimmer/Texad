using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class ActionManager
    {
        public TexadCharacter owner;
        public List<TexadAction> availableActions;

        public ActionManager(TexadCharacter owner)
        {
            this.owner = owner;
            availableActions = new List<TexadAction>();
        }

        public void addBasicActions()
        {
            addActionToAvailable(new MoveAction(200));
        }

        public void addActionToAvailable(TexadAction a)
        {
            availableActions.Add(a);
        }

        public void removeActionFromAvailable(TexadAction a)
        {
            availableActions.Remove(a);
        }

    }
}
