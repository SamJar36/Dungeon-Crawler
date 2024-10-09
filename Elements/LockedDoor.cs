using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class LockedDoor : LevelElement
    {
        public LockedDoor(int x, int y, Player player) : base(x, y, '?', ConsoleColor.White, player)
        {

        }
        public void TryOpeningDoor()
        {
            if (this.IsDrawing)
            {
                if (this.Player.KeyCount > 0)
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("You unlocked the door!");
                    this.Player.KeyCount -= 1;
                    this.Symbol = ' ';
                    this.Draw();
                    this.IsDrawing = false;
                }
                else
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("The door is locked");
                    this.Player.PosX = this.Player.LastPosX;
                    this.Player.PosY = this.Player.LastPosY;
                    this.Player.Steps--;
                }
            }     
        }
    }
}
