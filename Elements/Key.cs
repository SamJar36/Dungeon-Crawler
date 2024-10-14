using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Key : LevelElement
    { 
        public Key(int x, int y, Player player) 
            : base(x, y, 
                  'k', 
                  ConsoleColor.White, 
                  player)
        {

        }
        public void PickUpKey()
        {
            if (this.IsDrawing)
            {     
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"You picked up a Small Key!");
                this.Player.KeyCount += 1;
                this.Symbol = ' ';
                this.IsDrawing = false;
            }   
        }
    }
}
