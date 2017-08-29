using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    class LocationManager
    {
        protected TexadServer server;
        protected TexadScene currentScene;
        protected TexadWorld world;

        public LocationManager(TexadServer server, TexadScene s)
        {
            world = s.sector.biome.world;
            currentScene = s;
            this.server = server;
        }

        public virtual void moveScene(SceneConnection connection)
        {
            if (connection == null)
            {
                return;
            }
            if (!connection.locked)
            {
                TexadScene oldScene = currentScene;
                currentScene = connection.endpoint;
                //client.clientActionNotification("Moved into " + currentScene.sceneName);
                //currentScene.playerEnteredScene(client, oldScene);
                //server.sendClientLocationUpdate(client);
                //server.sendLocationDescription(client);
            }
            else
            {
            }
        }

        public string getLocationDescription()
        {
            return "You are in " + currentScene.getSceneDescription();
        }
    }

    class PlayerLocationManager : LocationManager
    {
        private TexadClient client;

        public PlayerLocationManager(TexadServer server, TexadClient client, TexadScene s) : base(server,s)
        {
            this.client = client;
        }

        public override void moveScene(SceneConnection connection)
        {
            //base.moveScene(connection);
            if (connection == null)
            {
                server.sendClientStoryUpdate("You cannot go that way!", client);
                return;
            }
            if (!connection.locked)
            {
                TexadScene oldScene = currentScene;
                currentScene = connection.endpoint;
                client.clientActionNotification("Moved into " + currentScene.sceneName);
                currentScene.playerEnteredScene(client, oldScene);
                server.sendClientLocationUpdate(client);
                server.sendLocationDescription(client);
            }
            else
            {
                Console.WriteLine("Cound not move, entry blocked");
                server.sendClientStoryUpdate("You cannot go that way, it is locked", client);
            }
        }
    }
}
