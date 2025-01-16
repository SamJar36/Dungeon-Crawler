using DungeonCrawler;
using Newtonsoft.Json;

namespace Dungeon_Crawler.Elements;

public abstract class LevelElement
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    [JsonIgnore] 
    public Player Player { get; set; }
    public bool IsDrawing { get; set; }
    public bool HasBeenDrawn { get; set; }
    public int DrawingDistance { get; set; }
    public LevelElement(int x, int y, char symbol, ConsoleColor color, Player player, int drawingDistance = 5)
    {
        this.PosX = x;
        this.PosY = y;
        this.Symbol = symbol;
        this.Color = color;
        this.Player = player;
        this.IsDrawing = true;
        this.DrawingDistance = drawingDistance;
        this.HasBeenDrawn = false;
    }
    public void Draw()
    {
        if (this.IsDrawing)
        {
            double distance = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
            if (distance <= DrawingDistance)
            {
                Console.ForegroundColor = Color;
                Console.SetCursorPosition(this.PosX, this.PosY);
                Console.Write(Symbol);
                Console.ResetColor();
                this.HasBeenDrawn = true;
            }
        }      
    }
    public void CheckIfPreviouslyDrawn()
    {
        if (this.HasBeenDrawn)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(this.PosX, this.PosY);
            Console.Write(Symbol);
            Console.ResetColor();
            this.HasBeenDrawn = false;
        }
    }
    public double CalculateEuclideanDistance(int x, int y, int playerX, int playerY)
    {
        int deltaX = x - playerX;
        int deltaY = y - playerY;
        double result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        return result;
    }
}

