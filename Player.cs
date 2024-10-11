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
    private char Symbol { get; set; }
    private LevelData LData { get; set; }
    public int HitPoints { get; set; }
    public int Steps { get; set; }
    public int KillCount { get; set; }
    public int GoldCount { get; set; }
    public int KeyCount { get; set; }
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

        this.LData = levelData;
        this.Symbol = '@';

        this.HitPoints = 100;
        this.Steps = 0;
        this.KillCount = 0;
        this.KeyCount = 0;
        this.HealthPotionCount = 0;

        Equipment EQ = new Equipment();
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
        Console.Write(this.Symbol);        
    }
    public void EraseLastPositionOfPlayer()
    {
        Console.SetCursorPosition(this.LastPosX, this.LastPosY);
        Console.Write(" ");
    }
    private void LastPositionOfPlayer()
    {
        this.LastPosY = this.PosY;
        this.LastPosX = this.PosX;
    }
    public void MovePlayer()
    {
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
                int healthRecovered = random.Next(20, 51);
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
            if (element is Wall wall)
            {
                IfMovementBlockedGoBack(wall);
            }
            else if (element is TreasureChest treasure)
            {
                if (this.PosX == treasure.PosX && this.PosY == treasure.PosY)
                {
                    treasure.OpenTreasureChest();
                }
            }
            else if (element is Gold gold)
            {
                if (this.PosX == gold.PosX && this.PosY == gold.PosY)
                {
                    gold.PickUpGold();
                }
            }
            else if (element is LockedDoor door)
            {
                if (this.PosX == door.PosX && this.PosY == door.PosY)
                {
                    door.TryOpeningDoor();
                }
            }
            else if (element is HeartPiece heart)
            {
                if (this.PosX == heart.PosX && this.PosY == heart.PosY)
                {
                    heart.PickUpHeartPiece();
                }
            }
        foreach (var enemy in LData.EnemyList)
            {
                if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
                {
                    this.PosX = this.LastPosX;
                    this.PosY = this.LastPosY;
                    this.Steps--;

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
        this.IsCurrentlyInABattle = true;
        enemy.IsAbleToMove = false;
        int playerAttack = EquippedWeapon.Throw();
        int enemyDefense = enemy.DefenseDice.Throw();
        int result = playerAttack - enemyDefense;
        if (result < 0)
        {
            result = 0;
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
                result = 0;
            }
            Console.WriteLine($"The {enemy.Name} attacked you for {enemyAttack}, you defended for {playerDefense}. The {enemy.Name} dealt {result} damage!");
            this.HitPoints -= result;
        }
        else
        {
            Console.SetCursorPosition(0, defensePositionOnHUD);
            Console.WriteLine($"The {enemy.Name} is defeated!");
            this.KillCount += 1;
        }
    }
    public void IfMovementBlockedGoBack(LevelElement element)
    {
        if (this.PosX == element.PosX && this.PosY == element.PosY)
        {
            this.PosX = this.LastPosX;
            this.PosY = this.LastPosY;
            this.Steps--;
        }
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