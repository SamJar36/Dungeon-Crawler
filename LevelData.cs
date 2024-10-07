using Dungeon_Crawler.Elements;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace DungeonCrawler;

public class LevelData
{
    private int currentLevel = 1;
    public int Level { get { return currentLevel; } }
    private int yOffset = 5;
    private List<Wall> wallList = new List<Wall>();
    public List<Wall> WallList {  get { return wallList; } }
    private List<Rat> ratList = new List<Rat>();
    public List<Rat> RatList { get {return ratList; } }
    public Player Player { get; set; }

    public void LoadMap()
    {     
        string filePath = @$"C:\Users\saman\source\repos\Dungeon-Crawler\Levels\Level{currentLevel}.text";

        int mapX = 0;
        int mapY = yOffset;
        int character;

        // creates the player first so the game doesn't crash when loading a rat, for example, before the player
        using (StreamReader readPlayerPositionFirst = new StreamReader(filePath))
        {
            while ((character = readPlayerPositionFirst.Read()) != -1)
            {
                if (character == '\n')
                {
                    mapY++;
                    mapX = 0;
                }
                else
                {
                    if (character == '@')
                    {
                        Player player = new Player(mapX, mapY, this);
                        this.Player = player;
                    }
                    mapX++;
                }
            }
        }
        mapX = 0;
        mapY = yOffset;
        using (StreamReader readEverythingElse = new StreamReader(filePath))
        {
            while ((character = readEverythingElse.Read()) != -1)
            {
                if (character == '\n')
                {
                    mapY++;
                    mapX = 0;
                }
                else
                {
                    if (character == '#')
                    {
                        Wall wall = new Wall(mapX, mapY);
                        wall.Draw();
                        wallList.Add(wall);
                    }
                    else if (character == 'C')
                    {
                        TreasureChest chest = new TreasureChest(mapX, mapY);
                        chest.Draw();
                    }
                    else if (character == 'r')
                    {
                        Rat rat = new Rat(mapX, mapY, this, this.Player);
                        rat.Draw();
                        ratList.Add(rat);

                    }   
                    mapX++;
                }
            }
        }
    }
}
