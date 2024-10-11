using DungeonCrawler;

namespace Dungeon_Crawler.Elements;

public abstract class LevelElement
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    protected Player Player { get; set; }
    protected bool IsDrawing { get; set; }
    protected LevelData LData { get; set; }
    public LevelElement(int x, int y, char symbol, ConsoleColor color, Player player)
    {
        this.PosX = x;
        this.PosY = y;
        this.Symbol = symbol;
        this.Color = color;
        this.Player = player;
        this.IsDrawing = true;
    }
    public void Draw()
    {
        if (this.IsDrawing)
        {
            double distance = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
            if (distance <= 5)
            {
                Console.ForegroundColor = Color;
                Console.SetCursorPosition(this.PosX, this.PosY);
                Console.Write(Symbol);
                Console.ResetColor();
            }
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

