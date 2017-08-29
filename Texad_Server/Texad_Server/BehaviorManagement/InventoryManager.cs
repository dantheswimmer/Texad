using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class InventoryManager
    {
        TexadCharacter owner;
        List<TexadItem> items;

        public InventoryManager(TexadCharacter owner)
        {
            this.owner = owner;
            items = new List<TexadItem>();
            addStartingItems();
        }

        public void addStartingItems()
        {
            TexadItem breadItem = new TexadItem("Barley Bread", 1, new QuantityAttribute("5"));
            TexadItem moneyItem = new TexadItem("Copper Coin", 2, new QuantityAttribute("15"));
            addItem(breadItem);
            addItem(moneyItem);
        }

        public void addItem(TexadItem item)
        {
            items.Add(item);
            item.itemAdded(owner);
        }

        public void removeItem(TexadItem item)
        {
            items.Remove(item);
            item.itemRemoved();
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
