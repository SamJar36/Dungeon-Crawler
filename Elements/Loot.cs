using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Loot
    {
        private Random random = new Random();
        private LevelData LData { get; set; }

        public Loot(LevelData levelData)
        {
            this.LData = levelData;
        }

        public void CreateLoot(int x, int y, Player player, int lowValue = 0, int highValue = 0)
        {
            int chance = random.Next(1, 101);
            if (chance <= 75)
            {
                Gold gold = new Gold(x, y, player, lowValue, highValue);
                
            }
            else if (chance >= 76)
            {
                HeartPiece heart = new HeartPiece(x, y, player);
            }
        }

    }
}
