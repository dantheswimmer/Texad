using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class LocationManager
    {
        TexadCharacter owner;
        protected TexadServer server;
        protected TexadScene currentScene;
        protected TexadWorld world;

        public LocationManager(TexadServer server, TexadCharacter owner, TexadScene s)
        {
            this.owner = owner;
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

        public PlayerLocationManager(TexadServer server, TexadCharacter owner, TexadClient client, TexadScene s) : base(server, owner, s)
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
