using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class GameLoop
    {
        public bool IsGameRunning { get; set; }
        private LevelData LevelData { get; set; }
        private HUD Hud { get; set; }
        public GameLoop(LevelData levelData, HUD hud)
        {
            this.IsGameRunning = true;
            this.LevelData = levelData;
            this.Hud = hud;
        }
        public void Run()
        {
            while (IsGameRunning)
            {
                Hud.Draw(LevelData.Player.HitPoints, LevelData.Player.Steps);
                LevelData.Player.MovePlayer();
                LevelData.Player.EraseLastPositionOfPlayer();
                LevelData.Player.DrawPlayer(LevelData.Player.PosX, LevelData.Player.PosY);
                foreach (var rat in LevelData.RatList)
                {
                    rat.Move();
                    rat.EraseLastPositionOfEnemy();
                    rat.Draw();
                }

            }
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
        }
    }
}
