using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class MagicalBarrier : LevelElement
    {
        public MagicalBarrier(int x, int y, Player player) : base(x, y, 'X', ConsoleColor.DarkMagenta, player)
        {

        }
        public void TryOpeningBarrier()
        {
            if (this.IsDrawing)
            {
                if (this.Player.MagicalKey == 1)
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("With your Magical Key you vanquished the Magical Barrier!");
                    this.Player.MagicalKey -= 1;
                    this.Symbol = ' ';
                    this.Draw();
                    this.IsDrawing = false;
                    this.Player.IfMovementBlockedGoBack();
                }
                else
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("Your path is blocked by a Magical Barrier");
                    this.Player.IfMovementBlockedGoBack();
                }
            }
        }
    }
}
