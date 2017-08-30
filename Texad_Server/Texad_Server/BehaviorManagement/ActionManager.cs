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
        public LocationManager locationManager;
        public List<TexadAction> availableActions;

        public ActionManager(LocationManager lm, TexadCharacter owner)
        {
            this.owner = owner;
            locationManager = lm;
            availableActions = new List<TexadAction>();
            addBasicActions();
        }

        public void addBasicActions()
        {
            addActionToAvailable(new PlayerMoveAction(2000));
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
