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
        public int GoldAmount { get; set; }

        public Gold(int x, int y, Player player, int lowValue, int highValue) 
            : base(x, y, 
                  '$', 
                  ConsoleColor.Yellow, 
                  player)
        {
            Random random = new Random();
            int goldAmount = random.Next(lowValue, highValue);
            this.GoldAmount = goldAmount;
        }
        public void PickUpGold()
        {
            if (this.IsDrawing)
            {
                Player.SoundEffects.PlaySoundEffect("Coins");
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"You picked up {GoldAmount} gold");
                this.Player.GoldCount += GoldAmount;
                this.Symbol = ' ';
                this.IsDrawing = false;
            }   
        }
    }
}
