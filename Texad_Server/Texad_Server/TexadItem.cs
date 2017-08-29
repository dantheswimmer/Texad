using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadItemAttribute
    {
        public string name;
        public string currentValue;
        public string maxValue;

        public TexadItemAttribute(string n, string cv = "none", string mv = "none")
        {
            name = n;
            currentValue = cv;
            maxValue = mv;
        }
    }

    public class TexadItem
    {
        public int itemID;
        public string quantity = "1";
        public List<TexadAction> capableOf = new List<TexadAction>();
        public List<TexadItemAttribute> attributes;
        public TexadCharacter owner;

        public TexadItem(string n, int id, TexadItemAttribute[] atts = null)
        {
            itemID = id;
            attributes = new List<TexadItemAttribute>();
            attributes.Add(new TexadItemAttribute(n));
            if(atts != null)
            {
                foreach(TexadItemAttribute newAtt in atts)
                {
                    attributes.Add(newAtt);
                }
            }
        }

        public TexadItem(string n, int id, TexadItemAttribute att)
        {
            itemID = id;
            attributes = new List<TexadItemAttribute>();
            attributes.Add(new TexadItemAttribute(n));
            attributes.Add(att);                
        }

        public void itemAdded(TexadCharacter newOwner)
        {
            owner = newOwner;
            addActionsToAvailable();
        }

        public void itemRemoved()
        {
            TexadItemAttribute qatt = getItemAttOfName("Quantity");
            if (qatt != null)
            {
                int val = Convert.ToInt16(qatt.currentValue);
                if ( val > 1)
                {
                    val--;
                    qatt.currentValue = val.ToString();
                    return;
                }
            }
            removeActionsFromAvailable();
            owner.inventoryManager.removeItem(this);
            owner = null;
        }

        public bool canDoAction(int actionID)
        {
            foreach(TexadAction ta in capableOf)
            {
                if (ta.actionID == actionID)
                    return true;
            }
            return false;
        }

        public TexadItemAttribute getItemAttOfName(string name)
        {
            foreach(TexadItemAttribute taa in attributes)
            {
                if (taa.name.Equals(name))
                    return taa;
            }
            return null;
        }

        public TexadAction getActionOfID(int actionID)
        {
            foreach(TexadAction action in capableOf)
            {
                if (action.actionID == actionID)
                    return action;
            }
            return TexadActionSystem.NO_ACTION;
        }

        public TexadAction getActionOfVerb(string verb)
        {
            foreach(TexadAction a in capableOf)
            {
                if (verb.Equals(a.actionName))
                    return a;
            }
            return TexadActionSystem.NO_ACTION;
        }

        public void addActionsToAvailable()
        {
            foreach(TexadAction a in capableOf)
            {
                owner.actionManager.addActionToAvailable(a);
            }
        }

        public void removeActionsFromAvailable()
        {
            foreach(TexadAction a in capableOf)
            {
                owner.actionManager.removeActionFromAvailable(a);
            }
        }

        public string serializeItemData()
        {
            string ret = "";
            foreach(TexadItemAttribute att in attributes)
            {
                ret += att.name;
                if (att.currentValue != "none")
                    ret += ": " + att.currentValue;
                if (att.maxValue != "none")
                    ret += "/" + att.maxValue;
                ret += ",";
            }

            return ret;
        }
    }
}
