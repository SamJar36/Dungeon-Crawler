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
        public FakeWall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.Gray, player)
        {
        }
        public void WalkThroughWall()
        {
            if (this.IsDrawing)
            {
                Player.SoundEffects.PlaySoundEffect("FakeWall");
                this.Player.MovementIsBlockedGoBack();
                this.Symbol = ' ';
                this.Draw();
                this.IsDrawing = false;
            }
        }
    }
}
