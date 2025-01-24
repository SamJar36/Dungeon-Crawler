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
                Player.SoundEffects.PlaySoundEffect("MagicalKey");
                Console.SetCursorPosition(0, 3);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("~~You found the Magical Key~~");
                Console.ForegroundColor = ConsoleColor.White;
                this.Player.MagicalKey += 1;
                this.Symbol = ' ';
                this.IsDrawing = false;

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(100);
                    foreach (var element in elements)
                    {
                        if (element is Wall vall && element.HasBeenDrawn == true && element.IsDrawing == true)
                        {
                            vall.DrawingDistance = 100;
                            vall.PaintWallsGreen();
                        }
                    }
                    Thread.Sleep(100);
                    foreach (var element in elements)
                    {
                        if (element is Wall vall && element.HasBeenDrawn == true && element.IsDrawing == true)
                        {
                            vall.PaintWallsGray();
                        }
                    }
                    
                }
            }
        }
    }
}
