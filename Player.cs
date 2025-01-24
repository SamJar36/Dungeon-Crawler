using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace DungeonCrawler;
[BsonIgnoreExtraElements]
public class Player
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int LastPosX { get; set; }
    public int LastPosY { get; set; }
    public bool IsAbleToMove { get; set; }
    public bool IsCurrentlyInABattle { get; set; }
    public bool IsArrowTileMoving { get; set; }
    public bool IsTouchingSolidObject { get; set; }
    public bool IsCurrentlyWarping { get; set; }
    public SoundEffects SoundEffects { get; set; }
    public Music Music { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    [BsonIgnore]
    public LevelData LData { get; set; }
    public int HitPoints { get; set; }
    public int Steps { get; set; }
    public int KillCount { get; set; }
    public int GoldCount { get; set; }
    public int KeyCount { get; set; }
    public int MagicalKey { get; set; }
    public int HealthPotionCount { get; set; }
    public Dice EquippedWeapon { get; set; }
    public Dice EquippedArmor { get; set; }
    private int attackPositionOnHUD = 3;
    private int defensePositionOnHUD = 4;

    public Player(int x, int y, LevelData levelData)
    {
        this.PosX = x;
        this.PosY = y;

        this.LastPosX = x;
        this.LastPosY = y;

        this.IsAbleToMove = true;
        this.IsCurrentlyInABattle = false;
        this.IsArrowTileMoving = false;
        this.IsTouchingSolidObject = false;
        this.IsCurrentlyWarping = false;

        this.LData = levelData;
        this.Symbol = '@';
        this.Color = ConsoleColor.White;
        this.HitPoints = 100;
        this.Steps = 0;
        this.KillCount = 0;
        this.KeyCount = 0;
        this.HealthPotionCount = 0;
        this.MagicalKey = 0;

        Equipment EQ = new Equipment();

        this.EquippedWeapon = EQ.WoodenSword;
        //this.EquippedWeapon = EQ.DebuggingSword;
        this.EquippedArmor = EQ.Tunic;

        this.SoundEffects = new SoundEffects();
        this.Music = new Music();

        DrawPlayer();
    }
    public Player()
    {
        this.Color = ConsoleColor.White;
        this.Symbol = '@';
        this.SoundEffects = new SoundEffects();
        this.Music = new Music();
    }
    public void Update()
    {
        MovePlayer();
        EraseLastPositionOfPlayer();
        DrawPlayer();
        this.IsTouchingSolidObject = false;
    }
    public void DrawPlayer()
    {
        Console.SetCursorPosition(this.PosX, this.PosY);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
        Console.ResetColor();     
    }
    public void EraseLastPositionOfPlayer()
    {
        Console.SetCursorPosition(this.LastPosX, this.LastPosY);
        Console.Write(" ");
    }
    public void LastPositionOfPlayer()
    {
        this.LastPosY = this.PosY;
        this.LastPosX = this.PosX;
    }
    public void MovePlayer()
    {
        if (IsArrowTileMoving) return;
        var keyPressed = Console.ReadKey();
        LastPositionOfPlayer();
        EraseBattleText();
        if (IsAbleToMove)
        {
            if (keyPressed.Key == ConsoleKey.X)
            {
                foreach (var enemy in LData.EnemyList)
                {
                    enemy.IsAbleToMove = false;
                }
                LData.GameDataService.SaveMap(LData);
                bool exitLoop = true;
                while (exitLoop)
                {
                    EraseBattleText();
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Game \u001b[32msaved\u001b[0m! Do you also wish to \u001b[31mexit\u001b[0m the game? y/n");
                    var keypressed2 = Console.ReadKey();
                    if (keypressed2.Key == ConsoleKey.Y)
                    {
                        exitLoop = false;
                        Console.Clear();
                        Console.WriteLine("Exiting the game. Thanks for playing!");
                        Environment.Exit(1);
                    }
                    else if (keypressed2.Key == ConsoleKey.N)
                    {
                        exitLoop = false;
                        EraseBattleText();
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("Understood! Resuming game.");
                    }
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                this.PosY -= 1;
                this.Steps++;
                CheckForCollision();
            }
            else if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                this.PosY += 1;
                this.Steps++;
                CheckForCollision();
            }
            else if (keyPressed.Key == ConsoleKey.LeftArrow)
            {
                this.PosX -= 1;
                this.Steps++;
                CheckForCollision();
            }
            else if (keyPressed.Key == ConsoleKey.RightArrow)
            {
                this.PosX += 1;
                this.Steps++;
                CheckForCollision();
            }
        }
        
        if (keyPressed.Key == ConsoleKey.D)
        {
            EraseBattleText();
            Console.SetCursorPosition(0, attackPositionOnHUD);
            foreach (var enemy in LData.EnemyList)
            {
                enemy.IsAbleToMove = false;
            }
            if (HealthPotionCount > 0)
            {
                Random random = new Random();
                int healthRecovered = random.Next(75, 101);
                this.SoundEffects.PlaySoundEffect("MagicalKey");
                Console.Write($"You drank a potion and recovered \u001b[32m{healthRecovered}\u001b[0m HP!");
                this.HitPoints += healthRecovered;
                this.HealthPotionCount -= 1;
            }
            else
            {
                Console.Write("You don't have any Health Potions left");
            }
        }
        else if (keyPressed.Key == ConsoleKey.K)
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Cheatcode activated! You got a key and a health potion!");
            this.KeyCount += 1;
            this.HealthPotionCount += 1;
        }
    }
    public void CheckForCollision()
    {
        foreach (var element in LData.LevelElementList)
        {
            if (this.PosX == element.PosX && this.PosY == element.PosY)
            {
                if (element is Wall wall)
                {
                    SoundEffects.PlaySoundEffect("Wall");
                    MovementIsBlockedGoBack();
                }
                else if (element is FakeWall fakeWall)
                {
                    fakeWall.WalkThroughWall();
                }
                else if (element is HiddenWall hiddenWall)
                {
                    SoundEffects.PlaySoundEffect("Wall");
                    MovementIsBlockedGoBack();
                }
                else if (element is TreasureChest treasure)
                {
                    treasure.OpenTreasureChest();
                }
                else if (element is Gold gold)
                {
                    gold.PickUpGold();
                }
                else if (element is LockedDoor door)
                {
                    door.TryOpeningDoor();
                }
                else if (element is HeartPiece heart)
                {
                    heart.PickUpHeartPiece();
                }
                else if (element is MagicalBarrier barrier)
                {
                    barrier.TryOpeningBarrier();
                }
                else if (element is MagicalKey magicalKey)
                {
                    magicalKey.PickUpMagicalKey(LData.LevelElementList);
                }
                else if (element is FinishLevel finish)
                {
                    finish.GoToNextLevel(LData);
                }
                else if (element is Warp warp)
                {
                    warp.UseWarp(LData.CurrentLevel);
                    SoundEffects.PlaySoundEffect("Warp");
                }
                else if (element is Key key)
                {
                    key.PickUpKey();
                }
                else if (element is ShopKeeper shopKeeper)
                {
                    shopKeeper.Talk();
                    MovementIsBlockedGoBack();
                }
                else if (element is ArrowTile arrow)
                {
                    SoundEffects.PlaySoundEffect("Arrow");
                    arrow.ArrowMovement(LData);
                }
                else if (element is GreenWall gwall)
                {
                    if (!IsArrowTileMoving)
                    {
                        gwall.Touch();
                    }
                }
                else if (element is PushableBlock pBlock)
                {
                    MovementIsBlockedGoBack();

                    pBlock.Push(LData);
                }
            }
        }
        foreach (var enemy in LData.EnemyList)
        {
            if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
            {
                MovementIsBlockedGoBack();

                if (enemy is Mimic mimic)
                {
                    // mimics have a slightly different battle phase
                    mimic.ActivateMimic();
                }
                else
                {
                    Battle(enemy);
                }
            }
        }
    }
    private void Battle(Enemy enemy)
    {
        if (IsCurrentlyWarping)
        {
            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The exit point of the Warp is blocked by a \u001b[31mmoving\u001b[0m object.");
            MovementIsBlockedGoBack();
            IsCurrentlyWarping = false;
            return;
        }
        Random random = new Random();
        int battleNoiseChance = random.Next(1, 3);
        if (battleNoiseChance == 1)
        {
            this.SoundEffects.PlaySoundEffect("Fight1");
        }
        else if (battleNoiseChance == 2)
        {
            this.SoundEffects.PlaySoundEffect("Fight2");
        }
        Console.ForegroundColor = ConsoleColor.White;
        this.IsCurrentlyInABattle = true;
        enemy.IsAbleToMove = false;
        int playerAttack = EquippedWeapon.Throw();
        int enemyDefense = enemy.DefenseDice.Throw();
        int result = playerAttack - enemyDefense;
        if (result <= 0)
        {
            result = 0;
        }
        else if (result > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.SetCursorPosition(0, attackPositionOnHUD);
        Console.WriteLine($"You attacked the {enemy.Name} for {playerAttack}, the {enemy.Name} defended for {enemyDefense}. You dealt {result} damage!");
        enemy.HitPoints -= result;
        if (enemy.HitPoints > 0)
        {
            Console.SetCursorPosition(0, defensePositionOnHUD);
            int enemyAttack = enemy.AttackDice.Throw();
            int playerDefense = EquippedArmor.Throw();
            result = enemyAttack - playerDefense;
            if (result < 0)
            {
                Console.ForegroundColor = ConsoleColor.White;

                result = 0;
            }
            else if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

            }
            Console.WriteLine($"The {enemy.Name} attacked you for {enemyAttack}, you defended for {playerDefense}. The {enemy.Name} dealt {result} damage!");
            this.HitPoints -= result;
            Console.ForegroundColor = ConsoleColor.White;

        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, defensePositionOnHUD);
            Console.WriteLine($"The {enemy.Name} is defeated!");
            this.KillCount += 1;
        }
    }
    public void MovementIsBlockedGoBack()
    {
        this.PosX = this.LastPosX;
        this.PosY = this.LastPosY;
        this.Steps--;
        this.IsTouchingSolidObject = true;
    }
    public void EraseBattleText()
    {
        // Erasing the battle text on the HUD each round
        string eraseBattleText = "                                                                                                                     ";
        Console.SetCursorPosition(0, 3);
        Console.Write(eraseBattleText);
        Console.SetCursorPosition(0, 4);
        Console.Write(eraseBattleText);
    }
}