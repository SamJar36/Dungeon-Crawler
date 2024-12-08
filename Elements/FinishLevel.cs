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
        public void GoToNextLevel(LevelData LData)
        {
            LData.IsSwitchingLevels = true;
            LData.SetCurrentLevel(LData.CurrentLevel + 1);
            Player.Music.PlayMusic("Fanfare");
        }
    }
}
