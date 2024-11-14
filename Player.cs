using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DungeonCrawler;

public class Player
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int LastPosX { get; set; }
    public int LastPosY { get; set; }
    public bool IsAbleToMove { get; set; }
    public bool IsCurrentlyInABattle { get; set; }
    public bool IsArrowTileMoving { get; set; }
    private char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    private LevelData LData { get; set; }
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

    public Player(int x, int y, LevelData levelData, Equipment EQ)
    {
        this.PosX = x;
        this.PosY = y;

        this.LastPosX = x;
        this.LastPosY = y;

        this.IsAbleToMove = true;
        this.IsCurrentlyInABattle = false;
        this.IsArrowTileMoving = false;

        this.LData = levelData;
        this.Symbol = '@';
        this.Color = ConsoleColor.White;

        this.HitPoints = 100;
        this.Steps = 0;
        this.KillCount = 0;
        this.KeyCount = 0;
        this.HealthPotionCount = 0;
        this.MagicalKey = 0;

        this.EquippedWeapon = EQ.WoodenSword;
        //this.EquippedWeapon = EQ.DebuggingSword;
        this.EquippedArmor = EQ.Tunic;

        DrawPlayer();
    }
    public void Update()
    {
        MovePlayer();
        EraseLastPositionOfPlayer();
        DrawPlayer();
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
            if (keyPressed.Key == ConsoleKey.UpArrow)
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
        if (keyPressed.Key == ConsoleKey.K)
        {
            this.KeyCount += 1;
            this.HealthPotionCount += 1;
        }
        else if (keyPressed.Key == ConsoleKey.D)
        {
            EraseBattleText();
            Console.SetCursorPosition(0, attackPositionOnHUD);
            if (HealthPotionCount > 0)
            {
                Random random = new Random();
                int healthRecovered = random.Next(75, 101);
                Console.Write($"You drank a potion and recovered {healthRecovered} HP!");
                this.HitPoints += healthRecovered;
                this.HealthPotionCount -= 1;
            }
            else
            {
                Console.Write("You don't have any Health Potions left");
            }
        }
    }
    private void CheckForCollision()
    {
        foreach (var element in LData.LevelElementList)
        {
            if (this.PosX == element.PosX && this.PosY == element.PosY)
            {
                if (element is Wall wall)
                {
                    IfMovementBlockedGoBack();
                }
                else if (element is FakeWall fakeWall)
                {
                    fakeWall.WalkThroughWall();
                }
                else if (element is HiddenWall hiddenWall)
                {
                    IfMovementBlockedGoBack();
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
                }
                else if (element is Key key)
                {
                    key.PickUpKey();
                }
                else if (element is ShopKeeper shopKeeper)
                {
                    shopKeeper.Talk();
                    IfMovementBlockedGoBack();
                }
                else if (element is ArrowTile arrow)
                {
                    arrow.ArrowMovement();
                }
                
            }
        foreach (var enemy in LData.EnemyList)
            {
                if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
                {
                    IfMovementBlockedGoBack();

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
    }
    private void Battle(Enemy enemy)
    {
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
    public void IfMovementBlockedGoBack()
    {
        this.PosX = this.LastPosX;
        this.PosY = this.LastPosY;
        this.Steps--;
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