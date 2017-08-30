using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadWorld //The world is everything
    {
        public int worldTime = 0;
        TexadBiome[][] worldBiomes;
        int numBiomesX;
        int numBiomesY;

        public TexadBiome startBiome;
        public TexadSector startSector;
        public TexadScene startScene;

        public TexadWorld()
        {
            initStartScene();
        }

        public void initStartScene()
        {
            startBiome = TexadBiome.getStartBiome();
            startSector = TexadSector.getStartSector(startBiome);
            startScene = TexadSector.addTestScenes(startSector);
        }

       
    }

    public class TexadBiome //Biomes are large areas that make up the world
    {
        public TexadWorld world;
        public string biomeName;
        public string biomeDescription;
        TexadSector[][] biomesSectors;
        int numSectorsX;
        int numSectorsY;

        public TexadBiome(string name)
        {
            biomeName = name;
        }

        public static TexadBiome getStartBiome()
        {
            return (new TexadBiome("Start Biome"));
        }
    }

    public class TexadSector //Sectors are medium areas that make up biomes
    {
        public TexadBiome biome;
        public string sectorName;
        public string sectorDescription;

        public static TexadSector getStartSector(TexadBiome biome)
        {
            biome.biomeName = "Test Biome";
            TexadSector s = new TexadSector();
            s.sectorName = "Test Sector";
            s.biome = biome;
            return s;
        }

        public static TexadScene addTestScenes(TexadSector sector)
        {
            TexadScene s1 = new TexadScene(sector, "Jail Cell", sceneTopology.ROOM_SQUARE);
            s1.nameIsGeneric = false;
            s1.addActorToScene(new TexadActor("Key", "the key to the cell door, carelessly left by the jailor."));
            TexadScene s2 = s1.connectToNewScene("Hallway", ConnectionType.PASSAGE , "Cell Door Exit", ConnectionType.PASSAGE, "Cell Door Entrance");
            //s1.connections[0].locked = true;
            TexadScene s4 = s2.connectToNewScene("Jail Basement", ConnectionType.DIRECTION, "South", ConnectionType.PASSAGE, "Hallway Trap Door");
            s4.nameIsGeneric = false;
            TexadScene s3 = s2.connectToNewScene("Jail Lobby", ConnectionType.DIRECTION, "North", ConnectionType.PASSAGE, "Jail Hallway Entrance");
            s3.nameIsGeneric = false;
            return s1; //Return start scene
        }
    }
}
