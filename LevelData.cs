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
    private List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList { get {return enemyList; } }
    private List<LevelElement> levelElementList = new List<LevelElement>();
    public List<LevelElement> LevelElementList { get { return levelElementList; } }
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
                        Wall wall = new Wall(mapX, mapY, this.Player);
                        wall.Draw();
                        levelElementList.Add(wall);
                    }
                    else if (character >= '1' && character <= '4')
                    {
                        if (currentLevel == 1)
                        {
                            if (character == '1')
                            {
                                CreateTreasureChestObject(mapX, mapY, "key");
                            }
                            if (character == '2')
                            {
                                CreateTreasureChestObject(mapX, mapY, "health potion");
                            }
                            if (character == '3')
                            {
                                CreateTreasureChestObject(mapX, mapY, "empty");
                            }
                            if (character == '4')
                            {
                                
                            }
                        }

                    }
                    else if (character == 'r')
                    {
                        Rat rat = new Rat(mapX, mapY, this, this.Player);
                        rat.Draw();
                        enemyList.Add(rat);

                    }
                    else if (character == 's')
                    {
                        Snake snake = new Snake(mapX, mapY, this, this.Player);
                        snake.Draw();
                        enemyList.Add(snake);
                    }
                    else if (character == 'M')
                    {
                        Mimic mimic = new Mimic(mapX, mapY, this, this.Player);
                        mimic.Draw();
                        enemyList.Add(mimic);
                    }
                    else if (character == '?')
                    {
                        LockedDoor door = new LockedDoor(mapX, mapY, this.Player);
                        door.Draw();
                        levelElementList.Add(door);
                    }
                    mapX++;
                }
            }
        }
    }
    private void CreateTreasureChestObject(int x, int y, string s)
    {
        TreasureChest chest = new TreasureChest(x, y, this.Player, s, this);
        chest.Draw();
        levelElementList.Add(chest);
    }
}
