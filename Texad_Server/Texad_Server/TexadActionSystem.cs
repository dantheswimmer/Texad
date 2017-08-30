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
        public TexadCharacter actionPerformer;

        public TexadAction(int id, string n, TexadCharacter aPerf = null, uint actionTime = 500, string[] aliases = null, int[] preReqs = null)
        {
            actionID = id;
            actionName = n;
            actionAliases = aliases;
            preReqActionIDs = preReqs;
            hasPrecidence = false;
            this.actionTime = actionTime;
            actionPerformer = aPerf;
        }

        public TexadAction()
        {

        }

        public virtual void doAction(object target, object source)
        {

        }
    }

    public class PlayerMoveAction : TexadAction
    {
        public PlayerMoveAction(uint aTime)
        {
            actionName = "move";
            actionTime = aTime;
        }

        public override void doAction(object target, object source)
        {
            string moveDir = (string)target;
            TexadPlayerCharacter mover = (TexadPlayerCharacter)source;
            mover.locationManager.moveScene(moveDir);
        }
    }

    public class EatAction : TexadAction
    {
        public EatAction(uint aTime, int fVal)
        {
            actionTime = aTime;
            actionName = "eat";
        }

        public override void doAction(object target, object source)
        {
            TexadItem eatTarget = (TexadItem)target;
            if(eatTarget == null) { return; } //Not even an item
            TexadItemAttribute fVal = eatTarget.getItemAttOfName("Food Value");
            int foodValue = Convert.ToInt16(fVal.currentValue);
            if(fVal == null)
            {
                //you cant eat this
                return;
            }
            TexadCharacter c = (TexadCharacter)source;
            TexadStat hungerStat = c.statManager.getStatWithName("Hunger");
            if (hungerStat == null)
            {
                //no hunger stat
                return;
            }
            hungerStat.addToStat(foodValue);
            eatTarget.itemRemoved();
            TexadPlayerCharacter player = (TexadPlayerCharacter)source;
            if(player != null)
            {
                player.playerDidAction("You have eaten " + eatTarget.attributes[0].name);
            }
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
}
