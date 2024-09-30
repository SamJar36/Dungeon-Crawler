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

    public Player(int x, int y)
    {
        this.PosX = x;
        this.PosY = y;

        this.LastPosX = x;
        this.LastPosY = y;

        DrawPlayer(this.PosX, this.PosY);
    }
    public void DrawPlayer(int x, int y)
    {
        EraseLastPosition();
        Console.SetCursorPosition(PosX, PosY);
        Console.Write(symbol);        
    }
    private void EraseLastPosition()
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
        }
        else if (keyPressed.Key == ConsoleKey.DownArrow)
        {
            this.PosY += 1;
        }
        else if (keyPressed.Key == ConsoleKey.LeftArrow)
        {
            this.PosX -= 1;
        }
        else if (keyPressed.Key == ConsoleKey.RightArrow)
        {
            this.PosX += 1;
        }
            
    }
}