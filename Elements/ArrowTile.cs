using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    internal class ArrowTile : LevelElement
    {
        private int DirectionX { get; set; }
        private int DirectionY { get; set; }
        public ArrowTile(int x, int y, Player player, int character) : base(x, y, '?', ConsoleColor.Green, player)
        {
            if (character == '↑')
            {
                this.DirectionX = 0;
                this.DirectionY = -1;
                this.Symbol = '↑';
            }
            else if (character == '→')
            {
                this.DirectionX = 1;
                this.DirectionY = 0;
                this.Symbol = '→';
            }
            else if (character == '↓')
            {
                this.DirectionX = 0;
                this.DirectionY = 1;
                this.Symbol = '↓';
            }
            else if (character == '←')
            {
                this.DirectionX = -1;
                this.DirectionY = 0;
                this.Symbol = '←';
            }
        }
        public void ArrowMovement()
        {
            Player.EraseLastPositionOfPlayer();
            Player.IsArrowTileMoving = true;
            Player.Color = ConsoleColor.Green;
            for(int i = 0; i < 10; i++)
            {
                Player.LastPositionOfPlayer();
                Player.PosX += this.DirectionX;
                Player.PosY += this.DirectionY;
                Player.DrawPlayer();
                if (i == 0)
                {
                    this.Draw();
                }
                else
                {
                    Player.EraseLastPositionOfPlayer();
                }
                
                Thread.Sleep(30);
            }
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            Player.IsArrowTileMoving = false;
            Player.Color = ConsoleColor.White;
        }
    }
}
