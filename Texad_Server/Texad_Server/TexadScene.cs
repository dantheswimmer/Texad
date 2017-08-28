using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public enum sceneTopology
    {
        ROOM_SQUARE,
        SPACE_OPEN,
        CAVE,
        WATER,
        FOREST,
        MOUNTAIN
    };

    public enum ConnectionType
    {
        DIRECTION,
        PASSAGE
    };

    public class SceneConnection
    {
        public string connDir;
        public TexadScene endpoint;
        public bool hidden;
        public bool locked;
        public object traverseReq;
        public ConnectionType connType;

        public SceneConnection(string cd, TexadScene ep, ConnectionType ct, bool h = false)
        {
            connDir = cd;
            connType = ct;
            endpoint = ep;
            hidden = h;
        }
    }

    public class TexadScene
    {
        public TexadSector sector;
        public string sceneName;
        public sceneTopology topology;
        public List<SceneConnection> connections;
        public bool nameIsGeneric;

        public List<TexadActor> population;
        public List<TexadClient> playerPopulation;
        public List<StringObjRelation> stringObjRelations;

        public TexadScene(TexadSector sector, string n, sceneTopology topology, bool nameGeneric = true)
        {
            connections = new List<SceneConnection>();
            population = new List<TexadActor>();
            playerPopulation = new List<TexadClient>();
            stringObjRelations = new List<StringObjRelation>();
            this.sector = sector;
            sceneName = n;
            this.topology = topology;
            nameIsGeneric = nameGeneric;
            WorldGenerator.populateSceneWithDefault(this);
        }

        public void addActorToScene(TexadActor a)
        {
            StringObjRelation sor = new StringObjRelation(a.actorName, a);
            stringObjRelations.Add(sor);
            population.Add(a);
        }

        public void removeActorFromScene(TexadActor a)
        {
            foreach(StringObjRelation sor in stringObjRelations)
            {
                if(sor.strObject.Equals(a))
                {
                    stringObjRelations.Remove(sor);
                }
            }
            population.Remove(a);
            broadcastStoryToScene(a.actorName + " is no longer in the scene.");
        }

        public void playerEnteredScene(TexadClient c, TexadScene oldScene)
        {
            playerPopulation.Add(c);
            broadcastStoryToScene(c.clientName + " has entered the area from " + oldScene.sceneName);
            StringObjRelation sor = new StringObjRelation(c.clientName, c);
        }

        public void playerLeftScene(TexadClient c)
        {
            playerPopulation.Remove(c);
            broadcastStoryToScene(c.clientName + " has left the area");
            foreach(StringObjRelation sor in stringObjRelations)
            {
                if (sor.strObject.Equals(c))
                    stringObjRelations.Remove(sor);
            }
        }

        public TexadScene connectToNewScene(string newSceneName, ConnectionType ct, string connDir, ConnectionType rct, string reverseConnDir, bool hidd = false, sceneTopology newSceneTopology = sceneTopology.SPACE_OPEN)
        {
            TexadScene newScene = new TexadScene(this.sector, newSceneName, newSceneTopology);
            connections.Add(new SceneConnection(connDir, newScene, ct));
            newScene.connections.Add(new SceneConnection(reverseConnDir, this, rct));
            return newScene;
        }

        public SceneConnection getConnectionFromString(string str)
        {
            foreach(SceneConnection sc in connections)
            {
                if(sc.connDir == str)
                {
                    return sc;
                }
            }
            return null;
        }

        public string getSceneDescription()
        {
            string ret = "";
            if (nameIsGeneric)
                ret += "a ";
            else
                ret += "the ";

            ret += sceneName + ".";
            foreach (SceneConnection sc in connections)
            {
                if (!sc.hidden)
                {
                    switch(sc.connType)
                    {
                        case ConnectionType.DIRECTION:
                        {
                            ret += " To the " + sc.connDir + " is";
                            break;
                        }
                        case ConnectionType.PASSAGE:
                        {
                            ret += " Through the " + sc.connDir + " is";
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                    if (sc.endpoint.nameIsGeneric)
                        ret += " a ";
                    else
                        ret += " the ";
                    ret += sc.endpoint.sceneName + ". ";
                }
            }

            ret += "The surrounding area contains: ";
            foreach(TexadActor a in population)
            {
                if(!a.hidden)
                {
                    ret += a.getSceneDescriptionString();
                }
            }
            return ret;
        }

        public void broadcastStoryToScene(string msg)
        {
            foreach(TexadClient c in playerPopulation)
            {
                c.myServer.sendClientStoryUpdate(msg, c);
            }
        }
    }
}
