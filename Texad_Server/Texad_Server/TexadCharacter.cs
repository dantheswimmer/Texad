﻿using System;
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
        protected LocationManager locationManager;
        protected TexadServer server;

        public TexadCharacter(TexadServer server, TexadScene currentScene)
        {
            statManager = new StatManager(this);
            actionManager = new ActionManager(this);
            inventoryManager = new InventoryManager(this);
            locationManager = new LocationManager(server, this, currentScene);
            this.server = server;
        }
    }

    public class TexadPlayerCharacter : TexadCharacter
    {
        public TexadPlayerCharacter(TexadClient client, TexadServer s, TexadScene sc) : base(s, sc)
        {
            locationManager = new PlayerLocationManager(server, this, client, sc);
        }
    }
}
