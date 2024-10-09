using DungeonCrawler;

namespace Dungeon_Crawler.Elements;

public class TreasureChest : LevelElement
{
    public TreasureChest(int x, int y, Player player) : base(x, y, 'C', ConsoleColor.Cyan, player)
    {

    }
}