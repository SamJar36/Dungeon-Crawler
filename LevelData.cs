﻿using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using Newtonsoft.Json;
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
    public GameDataService GameDataService { get; set; }

    public LevelData(GameDataService gameDataService)
    {
        this.CurrentLevel = 1;
        this.IsSwitchingLevels = false;

        this.GameDataService = gameDataService;
    }
    public void SetCurrentLevel(int level)
    {
        this.CurrentLevel = level;
    }
    public void LoadMapFromText()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), @$"Levels\Level{CurrentLevel}.text");

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
                            Player player = new Player(mapX, mapY, this);
                            this.Player = player;
                            
                        }
                        else
                        {
                            if (Player == null)
                            {
                                // in case I start on a different level for debugging or designing purposes
                                Player player = new Player(mapX, mapY, this);
                                this.Player = player;
                            }
                            else
                            {
                                this.Player.PosX = mapX;
                                this.Player.PosY = mapY;
                            }
                        }
                        Player.Music.PlayMusic("Level");
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
                            CreateTreasureChestObject(mapX, mapY, "key");
                        }
                        else if (character == '2')
                        {
                            CreateTreasureChestObject(mapX, mapY, "health potion");
                        }
                        else if (character == '3')
                        {
                            CreateTreasureChestObject(mapX, mapY, "empty");
                        }
                        if (CurrentLevel == 1)
                        {
                            if (character == '4')
                            {
                                CreateTreasureChestObject(mapX, mapY, "gold");
                            }
                            if (character == '5')
                            {
                                CreateTreasureChestObject(mapX, mapY, "leather armor");
                            }
                            else if (character == '6')
                            {
                                CreateTreasureChestObject(mapX, mapY, "short sword");
                            }
                        }
                        else if (CurrentLevel == 2)
                        {
                            if (character == '4')
                            {
                                CreateTreasureChestObject(mapX, mapY, "brass armor");
                            }
                            else if (character == '5')
                            {
                                CreateTreasureChestObject(mapX, mapY, "sabre");
                            }
                        }
                        else if (CurrentLevel == 3)
                        {
                            if (character == '4')
                            {
                                CreateTreasureChestObject(mapX, mapY, "chain armor");
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
                        Gold gold = new Gold(mapX, mapY, Player, 1, 5);
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
                        if (CurrentLevel == 1)
                        {
                            BossRatKing ratKing = new BossRatKing(mapX, mapY, this, this.Player);
                            ratKing.Draw();
                            enemyList.Add(ratKing);
                        }
                        else if (CurrentLevel == 2)
                        { 
                            BossSkeletonLeader skeletonLeader = new BossSkeletonLeader(mapX, mapY, this, this.Player);
                            skeletonLeader.Draw();
                            enemyList.Add(skeletonLeader);
                        }
                        else if (CurrentLevel == 3)
                        {
                            BossSkeletonLeader skeletonLeader = new BossSkeletonLeader(mapX, mapY, this, this.Player);
                            skeletonLeader.Draw();
                            enemyList.Add(skeletonLeader);
                        }
                    }
                    else if (character == 'S')
                    {
                        ShopKeeper shopKeeper = new ShopKeeper(mapX, mapY, this.Player);
                        shopKeeper.Draw();
                        levelElementList.Add(shopKeeper);
                    }
                    else if (character == '→' || character == '↓' || character == '←' || character == '↑')
                    {
                        character = (char)character;
                        ArrowTile arrow = new ArrowTile(mapX, mapY, this.Player, character);
                        arrow.Draw();
                        LevelElementList.Add(arrow);
                    }
                    else if (character == '‡')
                    {
                        GreenWall gwall = new GreenWall(mapX, mapY, this.Player);
                        gwall.Draw();
                        LevelElementList.Add(gwall);
                    }
                    else if (character == '⁋')
                    {
                        PushableBlock pBlock = new PushableBlock(mapX, mapY, this.Player);
                        pBlock.Draw();
                        LevelElementList.Add(pBlock);
                    }
                    mapX++;
                }
            }
        }
    }
    //public void Save()
    //{
    //    LevelElementBsonObject jsonObject = new LevelElementBsonObject(LevelElementList);
    //    EnemyBsonObject enemyJsonObject = new EnemyBsonObject(EnemyList);
    //    var settings = new JsonSerializerSettings
    //    {
    //        Formatting = Formatting.Indented,
    //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    //    };
    //    string json1 = JsonConvert.SerializeObject(jsonObject, settings);
    //    string json2 = JsonConvert.SerializeObject(enemyJsonObject, settings);
    //    string json3 = JsonConvert.SerializeObject(this.Player, settings);
    //    File.WriteAllText("ldata-elements.txt", json1);
    //    File.WriteAllText("ldata-enemies.txt", json2);
    //    File.WriteAllText("ldata-player.txt", json3);
    //}
    //public void LoadSavedMap()
    //{
    //    string json1 = File.ReadAllText("ldata-elements.txt");
    //    string json2 = File.ReadAllText("ldata-enemies.txt");
    //    string json3 = File.ReadAllText("ldata-player.txt");

    //    LevelElementBsonObject jsonObject = JsonConvert.DeserializeObject<LevelElementBsonObject>(json1);
    //    EnemyBsonObject enemyJsonObject = JsonConvert.DeserializeObject<EnemyBsonObject>(json2);
    //    Player player = JsonConvert.DeserializeObject<Player>(json3);

    //    player.LData = this;
    //    this.Player = player;       
    //    Player.DrawPlayer();
    //    var jsonList = jsonObject.CombineLists();
    //    var enemyJsonList = enemyJsonObject.CombineLists();
    //    foreach (var element in jsonList)
    //    {
    //        element.Player = player;
    //        element.CheckIfPreviouslyDrawn();
    //        LevelElementList.Add(element);
    //    }
    //    foreach (var enemy in enemyJsonList)
    //    {
    //        enemy.Player = player;
    //        enemy.LData = this;
    //        enemy.Draw();
    //        EnemyList.Add(enemy);
    //    }
    //    Player.Music.PlayMusic("Level");
    //}
    private void CreateTreasureChestObject(int x, int y, string s)
    {
        TreasureChest chest = new TreasureChest(x, y, this.Player, s);
        chest.Draw();
        levelElementList.Add(chest);
    }
}
