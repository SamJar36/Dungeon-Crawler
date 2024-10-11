using DungeonCrawler;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Dungeon_Crawler.Elements;

public class Wall : LevelElement
{
    public Wall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.DarkGray, player)
    {

    }
    public void PaintWallsGreen()
    {
        this.Color = ConsoleColor.Green;
        this.Draw();
    }
    public void PaintWallsGray()
    {
        this.Color = ConsoleColor.DarkGray;
        this.Draw();
    }
}
