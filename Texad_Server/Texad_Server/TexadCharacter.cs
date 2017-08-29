using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadCharacter
    {
        public string displayName;
        public  StatManager statManager;
        public ActionManager actionManager;
        public InventoryManager inventoryManager;
        private LocationManager locationManager;
        private TexadServer server;

        public TexadCharacter(TexadServer server, TexadScene currentScene)
        {
            statManager = new StatManager();
            actionManager = new ActionManager();
            inventoryManager = new InventoryManager();
            locationManager = new LocationManager(server, currentScene);
            this.server = server;
        }
    }
}
