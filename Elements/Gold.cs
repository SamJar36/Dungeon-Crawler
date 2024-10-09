using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Gold : LevelElement
    { 
        private Random random = new Random();

        public Gold(int x, int y, Player player) 
            : base(x, y, 
                  '$', 
                  ConsoleColor.Yellow, 
                  player)
        {

        }
        public void PickUpGold(int lowValue, int highValue)
        {
            if (IsDrawing)
            {
                int goldAmount = random.Next(lowValue, highValue);
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"You picked up {goldAmount} gold");
                this.Player.GoldCount += goldAmount;
                this.Symbol = ' ';
                this.IsDrawing = false;
            }   
        }
    }
}
