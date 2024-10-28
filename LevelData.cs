using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace DungeonCrawler;

public class LevelData
{
    public int CurrentLevel { get; set; }
    public bool IsSwitchingLevels { get; set; }
    private int yOffset = 5;
    private List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList { get {return enemyList; } }
    private List<LevelElement> levelElementList = new List<LevelElement>();
    public List<LevelElement> LevelElementList { get { return levelElementList; } }
    public Player Player { get; set; }

    public LevelData()
    {
        this.CurrentLevel = 2;
        this.IsSwitchingLevels = false;
    }
    public void SetCurrentLevel(int level)
    {
        this.CurrentLevel = level;
    }
    public void LoadMap(Equipment EQ)
    {     
        string filePath = @$"C:\Users\saman\source\repos\Dungeon-Crawler\Levels\Level{CurrentLevel}.text";

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
                        if (CurrentLevel == 1)
                        {
                            Player player = new Player(mapX, mapY, this, EQ);
                            this.Player = player;
                        }
                        else
                        {
                            if (Player == null)
                            {
                                // in case I start on a different level for debugging or designing purposes
                                Player player = new Player(mapX, mapY, this, EQ);
                                this.Player = player;
                            }
                            else
                            {
                                this.Player.PosX = mapX;
                                this.Player.PosY = mapY;
                            }
                        }
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
                    else if (character >= '1' && character <= '9')
                    {
                        if (character == '1')
                        {
                            CreateTreasureChestObject(mapX, mapY, "key", EQ);
                        }
                        else if (character == '2')
                        {
                            CreateTreasureChestObject(mapX, mapY, "health potion", EQ);
                        }
                        else if (character == '3')
                        {
                            CreateTreasureChestObject(mapX, mapY, "empty", EQ);
                        }
                        if (CurrentLevel == 1)
                        {
                            if (character == '4')
                            {
                                CreateTreasureChestObject(mapX, mapY, "gold", EQ);
                            }
                        }
                        else if (CurrentLevel == 2)
                        {
                            if (character == '4')
                            {
                                CreateTreasureChestObject(mapX, mapY, "leather armor", EQ);
                            }
                            else if (character == '5')
                            {
                                CreateTreasureChestObject(mapX, mapY, "short sword", EQ);
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
                    else if (character == '♥')
                    {
                        HeartPiece heart = new HeartPiece(mapX, mapY, this.Player, false);
                        heart.Draw();
                        levelElementList.Add(heart);
                    }
                    else if (character == '♡')
                    {
                        HeartPiece heart = new HeartPiece(mapX, mapY, this.Player, true);
                        heart.Draw();
                        levelElementList.Add(heart);
                    }
                    else if (character == '$')
                    {
                        Gold gold = new Gold(mapX, mapY, Player, 1, 2);
                        gold.Draw();
                        levelElementList.Add(gold);
                    }
                    else if (character == 'X')
                    {
                        MagicalBarrier barrier = new MagicalBarrier(mapX, mapY, this.Player);
                        barrier.Draw();
                        levelElementList.Add(barrier);
                    }
                    else if (character == 'K')
                    {
                        MagicalKey magicalkey = new MagicalKey(mapX, mapY, this.Player);
                        magicalkey.Draw();
                        levelElementList.Add(magicalkey);
                    }
                    else if (character == 'F')
                    {
                        FinishLevel finish = new FinishLevel(mapX, mapY, this.Player);
                        finish.Draw();
                        levelElementList.Add(finish);
                    }
                    else if (character == '>')
                    {
                        FakeWall fakeWall = new FakeWall(mapX, mapY, this.Player);
                        fakeWall.Draw();
                        levelElementList.Add(fakeWall);
                    }
                    else if (character == '_')
                    {
                        HiddenWall hiddenWall = new HiddenWall(mapX, mapY, this.Player);
                        hiddenWall.Draw();
                        levelElementList.Add(hiddenWall);
                    }
                    // up to 6 possible sets of Warps in each level
                    else if (character == 'Ö' ||
                        character == 'Ä' ||
                        character == 'Å' ||
                        character == 'ö' ||
                        character == 'ä' ||
                        character == 'å')
                    {
                        character = (char)character;
                        Warp warp = new Warp(mapX, mapY, this.Player, filePath, character);
                        warp.Draw();
                        levelElementList.Add(warp);
                    }
                    else if (character == 'B')
                    {
                        if (CurrentLevel == 1 || CurrentLevel == 2)
                        {
                            BossRatKing ratKing = new BossRatKing(mapX, mapY, this, this.Player);
                            ratKing.Draw();
                            enemyList.Add(ratKing);
                        }
                    }
                    mapX++;
                }
            }
        }
    }
    private void CreateTreasureChestObject(int x, int y, string s, Equipment EQ)
    {
        TreasureChest chest = new TreasureChest(x, y, this.Player, s, this, EQ);
        chest.Draw();
        levelElementList.Add(chest);
    }
}
