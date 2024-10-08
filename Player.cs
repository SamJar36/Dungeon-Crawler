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
    private char Symbol { get; set; }
    private LevelData LData { get; set; }
    public int HitPoints { get; set; }
    public int Steps { get; set; }
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

        this.LData = levelData;
        this.Symbol = '@';

        this.HitPoints = 100;
        this.Steps = 0;

        Equipment EQ = new Equipment();
        //this.EquippedWeapon = EQ.WoodenSword;
        this.EquippedWeapon = EQ.DebuggingSword;
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
    private void CheckForCollision()
    {
        foreach (var wall in LData.WallList)
        {
            if (this.PosX == wall.PosX && this.PosY == wall.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;
                this.Steps--;
            }
        }
        foreach (var enemy in LData.EnemyList)
        {
            if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;
                this.Steps--;

                Battle(enemy);
            }
        }
    }
    private void Battle(Enemy enemy)
    {
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
            if (this.HitPoints < 1)
            {
                GameOver();
            }
        }
        else
        {
            Console.SetCursorPosition(0, defensePositionOnHUD);
            Console.WriteLine($"The {enemy.Name} is defeated!");
        }
    }
    public void EraseBattleText()
    {
        string eraseBattleText = "                                                                                                                     ";
        Console.SetCursorPosition(0, 3);
        Console.Write(eraseBattleText);
        Console.SetCursorPosition(0, 4);
        Console.Write(eraseBattleText);
    }
    public void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("############################");
        Console.WriteLine("#                          #");
        Console.WriteLine("#        GAME OVER         #");
        Console.WriteLine("#                          #");
        Console.WriteLine("#   Your HP went below 0   #");
        Console.WriteLine("#                          #");
        Console.WriteLine("############################");
        Console.ReadKey();


    }
}