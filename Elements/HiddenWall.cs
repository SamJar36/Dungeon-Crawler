using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class InnerWall : LevelElement
    {
        public InnerWall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.Gray, player)
        {
        }
    }
}
