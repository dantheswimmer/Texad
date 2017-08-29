using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public struct ItemActionPair
    {
        public TexadAction action;
        public TexadItem item;
        public ItemActionPair(TexadAction a, TexadItem i)
        {
            action = a;
            item = i;
        }
    }

    public class TexadAction
    {
        public int actionID;
        public string actionName;
        public string[] actionAliases;
        public int[] preReqActionIDs;
        public bool hasPrecidence;
        public uint actionTime;
        public object actionOwner;

        public TexadAction(int id, string n, object aOwner = null, uint actionTime = 500, string[] aliases = null, int[] preReqs = null)
        {
            actionID = id;
            actionName = n;
            actionAliases = aliases;
            preReqActionIDs = preReqs;
            hasPrecidence = false;
            this.actionTime = actionTime;
            actionOwner = aOwner;
        }

        public TexadAction()
        {

        }

        public virtual void doAction(object target, object source)
        {

        }
    }

    public class MoveAction : TexadAction
    {
        public MoveAction(uint aTime)
        {
            actionName = "move";
            actionTime = aTime;
        }

        public override void doAction(object target, object source)
        {
            string moveDir = (string)target;
            TexadClient mover = (TexadClient)source;
            mover.clientMove(mover.currentScene.getConnectionFromString(moveDir));
        }
    }

    public class EatAction : TexadAction
    {
        int foodValue;
        public EatAction(uint aTime, int fVal)
        {
            actionTime = aTime;
            actionName = "eat";
            foodValue = fVal;
        }

        public override void doAction(object target, object source)
        {
            TexadClient c = (TexadClient)source;
            TexadStat hungerStat = c.getStatWithName("Hunger");
            if (hungerStat == null)
                return;
            hungerStat.addToStat(foodValue);
            TexadItem foodItem = (TexadItem)actionOwner;
            foodItem.itemRemoved();
            c.clientActionNotification("You have eaten " + foodItem.attributes[0].name + ", raising your hunger to: " + c.getStatWithName("Hunger").currentValue);
            c.myServer.sendClientItemUpdate(c);
            c.myServer.sendClientStatUpdate(c);
        }
    }

    public class TexadActionEvent
    {
        public TexadAction myAction;
        public object target;
        public object source;
        public uint doneTime;
        public TexadActionEvent(TexadAction action, object target, object source)
        {
            myAction = action;
            this.target = target;
            this.source = source;
        }
    }

    public class TexadActionSystem
    {
        public static TexadAction NO_ACTION = new TexadAction(-1, "No action");

        public static int retrieveActionIDFromVerb(string verb, TexadClient client)
        {
            int ret = -1;
            //Check any custom actions the player may have
            foreach(TexadItem item in client.playerItems)
            {
                TexadAction a = item.getActionOfVerb(verb);
                if (!a.Equals(TexadActionSystem.NO_ACTION))
                    return a.actionID;
            }
            return ret;
        }

        public static List<TexadItem> canDoAction(TexadAction action, TexadClient attemptee)
        {
            List<TexadItem> candidates = new List<TexadItem>();
            foreach(TexadItem item in attemptee.playerItems)
            {
                if (item.canDoAction(action.actionID))
                    candidates.Add(item);
            }
            return candidates;
        }        
    }
}
