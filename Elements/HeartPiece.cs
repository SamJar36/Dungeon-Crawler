using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class HeartPiece : LevelElement
    {
        private int HealAmount { get; set; }
        private string Name { get; set; }
        public HeartPiece(int x, int y, Player player) 
            : base(x, y, 
                  '♥', 
                  ConsoleColor.White, 
                  player)
        {
            Random random = new Random();
            int chance = random.Next(1, 11);
            if (chance >= 1 && chance <= 9)
            {
                this.Color = ConsoleColor.Red;
                this.HealAmount = 10;
                this.Name = "Heart Piece";
            }
            else
            {
                this.Color = ConsoleColor.Yellow;
                this.HealAmount = 50;
                this.Name = "Golden Heart";
            }
        }
        public void PickUpHeartPiece()
        {
            if (this.IsDrawing)
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"You picked up a Heart Piece and healed for {this.HealAmount} hitpoints!");
                Player.HitPoints += HealAmount;
                this.Symbol = ' ';
                this.IsDrawing = false;
            }
        }
    }
}
