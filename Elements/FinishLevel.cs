using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class FinishLevel : LevelElement
    {
        public FinishLevel(int x, int y, Player player) : base(x, y, 'F', ConsoleColor.Green, player)
        {

        }
        public void GoToNextLevel()
        {
            // I didn't have time to make another level so this is a placeholder
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("#############################");
            Console.WriteLine("#                           #");
            Console.WriteLine("#      CONGRATULATIONS      #");
            Console.WriteLine("#                           #");
            Console.WriteLine("#  You finished the level!  #");
            Console.WriteLine("#                           #");
            Console.WriteLine("#############################");
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("(Unfortunately, I didn't have time to make anymore levels...)");
            Console.ReadKey();
            Environment.Exit(0);
            
        }
    }
}
