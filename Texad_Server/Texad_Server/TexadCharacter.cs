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
        public LocationManager locationManager;
        protected TexadServer server;

        public TexadCharacter(TexadServer server, TexadScene currentScene)
        {
            statManager = new StatManager(this);
            inventoryManager = new InventoryManager(this);
            locationManager = new LocationManager(server, this, currentScene);
            actionManager = new ActionManager(locationManager, this);
            this.server = server;
        }
    }

    public class TexadPlayerCharacter : TexadCharacter
    {
        public new PlayerLocationManager locationManager;
        public TexadClient client;

        public TexadPlayerCharacter(TexadClient client, TexadServer s, TexadScene sc) : base(s, sc)
        {
            this.client = client;
            locationManager = new PlayerLocationManager(server, this, client, sc);
        }

        public void playerDidAction(string notification)
        {
            server.sendClientStoryUpdate(notification, client);
            server.sendClientStatUpdate(client);
            server.sendClientItemUpdate(client);
        }
    }
}
