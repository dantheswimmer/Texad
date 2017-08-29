using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    class TexadCharacter
    {
        public string displayName;
        private StatManager statManager;
        private ActionManager actionManager;
        private InventoryManager inventoryManager;
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
