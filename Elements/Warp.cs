using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Warp : LevelElement
    {
        public Warp(int x, int y, Player player) : base(x, y, 'W', ConsoleColor.DarkCyan, player)
        {

        }
        public void UseWarp(int currentlevel)
        {
            if (this.Player.PosX == this.PosX && this.Player.PosY == this.PosY)
            {
                if (currentlevel == 1)
                {

                }
                else if (currentlevel == 2)
                {

                }
            }
        }
    }
}
