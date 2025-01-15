using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class GreenWall : LevelElement
    {
        public GreenWall(int x, int y, Player player) : base(x, y, '#', ConsoleColor.Green, player)
        {

        }
        public void Touch()
        {
            Player.SoundEffects.PlaySoundEffect("GreenWall");
            Console.SetCursorPosition(0, 3);
            Console.Write("It's just a \u001b[32mgreen\u001b[0m wall.");
            Player.MovementIsBlockedGoBack();
        }
    }
}
