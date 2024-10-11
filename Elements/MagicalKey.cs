using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class MagicalKey : LevelElement
    {
        public MagicalKey(int x, int y, Player player) : base(x, y, 'K', ConsoleColor.Blue, player)
        {

        }
        public void PickUpMagicalKey(List<LevelElement> elements)
        {
            if (this.IsDrawing)
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("~~You found the Magical Key~~");
                this.Player.MagicalKey += 1;
                this.Symbol = ' ';
                this.IsDrawing = false;

                // this was supposed to draw the entire map and do a cool blinking green effect, but I couldn't get it working properly in time
                //for (int i = 0; i < 5; i++)
                //{
                //    Thread.Sleep(100);
                //    foreach (var el in elements)
                //    {
                //        if (el is Wall vall)
                //        {
                //            vall.PaintWallsGray();
                //        }
                //    }
                //    Thread.Sleep(100);
                //    foreach (var el in elements)
                //    {
                //        if (el is Wall vall)
                //        {
                //            vall.DrawingDistance = 100;
                //            vall.PaintWallsGreen();
                //        }
                //    } 
                //}
            }
        }
    }
}
