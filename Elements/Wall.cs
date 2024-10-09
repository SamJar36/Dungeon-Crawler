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
}
