using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class HiddenWall : LevelElement
    {
        public HiddenWall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.Gray, player, 1)
        {
        }
    }
}
