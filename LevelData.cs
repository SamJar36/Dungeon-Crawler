using Dungeon_Crawler.Elements;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace DungeonCrawler;

public class LevelData
{
    private int currentLevel = 1;
    private List<Wall> wallList = new List<Wall>();
    public List<Wall> WallList {  get { return wallList; } }

    public void LoadMap()
    {     
        string filePath = @$"C:\Users\saman\source\repos\Dungeon-Crawler\Levels\Level{currentLevel}.text";
        Console.SetCursorPosition(0, 0);
        using (StreamReader reader = new StreamReader(filePath))
        {
            int mapX = 0;
            int mapY = 0;
            int character;
            while ((character = reader.Read()) != -1)
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
                    else if (character == 'B')
                    {
                        TreasureChest chest = new TreasureChest(mapX, mapY);
                        chest.Draw();
                    }
                    mapX++;

                }
            }
        }
    }
}
