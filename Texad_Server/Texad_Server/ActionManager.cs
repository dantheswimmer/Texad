using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class ActionManager
    {
        List<TexadAction> availableActions;

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
