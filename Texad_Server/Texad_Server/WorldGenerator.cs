using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    class WorldGenerator
    {
        public static void populateSceneWithDefault(TexadScene scene)
        {
            switch(scene.topology)
            {
                case sceneTopology.CAVE:
                    {
                        populateCave(scene);
                        break;
                    }
                case sceneTopology.FOREST:
                    {
                        populateForest(scene);
                        break;
                    }
                case sceneTopology.MOUNTAIN:
                    {
                        populateMountain(scene);
                        break;
                    }
                case sceneTopology.ROOM_SQUARE:
                    {
                        populateRoom(scene);
                        break;
                    }
                case sceneTopology.WATER:
                    {
                        populateWater(scene);
                        break;
                    }
            }
        }

        public static void populateCave(TexadScene cave)
        {

        }

        public static void populateForest(TexadScene forest)
        {

        }

        public static void populateMountain(TexadScene mountain)
        {

        }

        public static void populateRoom(TexadScene room)
        {

        }

        public static void populateWater(TexadScene water)
        {

        }
    }
}
