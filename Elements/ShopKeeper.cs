using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    internal class ShopKeeper : LevelElement
    {
        public bool IsShopOpen { get; set; }
        public ShopKeeper(int x, int y, Player player) : base(x, y, '@', ConsoleColor.Yellow, player)
        {
            this.IsShopOpen = false;
        }
        public void Talk()
        {
            Console.SetCursorPosition(0, 3);
            Console.Write("\u001b[33mShopkeeper:\u001b[0m Greetings Traveler! Please peruse my wares.");
        }
    }
}
