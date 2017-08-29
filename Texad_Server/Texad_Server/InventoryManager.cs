using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class InventoryManager
    {
        List<TexadItem> items;

        public InventoryManager()
        {
            items = new List<TexadItem>();
        }

        public void addItem(TexadItem item)
        {
            items.Add(item);
        }

        public void removeItem(TexadItem item)
        {
            items.Remove(item);
        }


        public string serializeInventory()
        {
            string serStr = "i";
            for (int i = 0; i < items.Count; i++)
            {
                serStr += items[i].serializeItemData();
                if (i < items.Count - 1)
                    serStr += "|";
            }
            return serStr;
        }
    }
}
