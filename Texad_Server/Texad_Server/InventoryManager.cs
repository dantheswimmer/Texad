using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    class InventoryManager
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
    }
}
