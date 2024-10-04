﻿namespace Dungeon_Crawler.Elements;

public abstract class LevelElement
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    public LevelElement(int x, int y, char symbol, ConsoleColor color)
    {
        this.PosX = x;
        this.PosY = y;
        this.Symbol = symbol;
        this.Color = color;
    }
    public void Draw()
    {
        Console.ForegroundColor = Color;
        Console.SetCursorPosition(this.PosX, this.PosY);
        Console.Write(Symbol);
        Console.ResetColor();
    }
}

