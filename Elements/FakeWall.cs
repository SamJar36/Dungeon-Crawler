using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class FakeWall : LevelElement
    {
        public FakeWall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.DarkGray, player)
        {
        }
        public void WalkThroughWall()
        {
            if (this.IsDrawing)
            {
                this.Player.IfMovementBlockedGoBack();
                this.Symbol = ' ';
                this.Draw();
                this.IsDrawing = false;
            }
        }
    }
}
