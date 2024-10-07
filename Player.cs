using Dungeon_Crawler.Elements;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DungeonCrawler;

public class Player
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    private int LastPosX { get; set; }
    private int LastPosY { get; set; }
    private char symbol = '@';
    private LevelData LData { get; set; }

    public Player(int x, int y, LevelData levelData)
    {
        this.PosX = x;
        this.PosY = y;

        this.LastPosX = x;
        this.LastPosY = y;

        this.LData = levelData;

        DrawPlayer(this.PosX, this.PosY);
    }
    public void DrawPlayer(int x, int y)
    {
        Console.SetCursorPosition(PosX, PosY);
        Console.Write(symbol);        
    }
    public void EraseLastPositionOfPlayer()
    {
        Console.SetCursorPosition(LastPosX, LastPosY);
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
        if (keyPressed.Key == ConsoleKey.UpArrow)
        {
            this.PosY -= 1;
            CheckForCollision();
        }
        else if (keyPressed.Key == ConsoleKey.DownArrow)
        {
            this.PosY += 1;
            CheckForCollision();

        }
        else if (keyPressed.Key == ConsoleKey.LeftArrow)
        {
            this.PosX -= 1;
            CheckForCollision();

        }
        else if (keyPressed.Key == ConsoleKey.RightArrow)
        {
            this.PosX += 1;
            CheckForCollision();

        }
    }
    private void CheckForCollision()
    {
        foreach (var item in LData.WallList)
        {
            if (this.PosX == item.PosX && this.PosY == item.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;
            }
        }
        foreach (var rat in LData.RatList)
        {
            if (this.PosX == rat.PosX && this.PosY == rat.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;

                // start battle
            }
        }
    }
}