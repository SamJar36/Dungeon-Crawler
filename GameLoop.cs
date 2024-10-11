using Dungeon_Crawler.Elements;
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
            this.LevelData = levelData;
            this.Hud = hud;
            this.IsGameRunning = true;
        }
        public void Run()
        {
            while (IsGameRunning)
            {
                if (LevelData.Player.HitPoints <= 0)
                {
                    GameOver();
                    continue;
                }
                Hud.Draw(
                    LevelData.Player.HitPoints, 
                    LevelData.Player.Steps, 
                    LevelData.Player.KillCount, 
                    LevelData.Player.GoldCount,
                    LevelData.Player.KeyCount,
                    LevelData.Player.HealthPotionCount);
                LevelData.Player.Update();
                foreach (var enemy in LevelData.EnemyList)
                {
                    enemy.Update();
                }
                foreach (var element in LevelData.LevelElementList)
                {
                    element.Draw();
                }
                // can't remove an enemy from list within the foreach loop above
                for (int i = LevelData.EnemyList.Count - 1; i >= 0; i--)
                {
                    if (LevelData.EnemyList[i].HitPoints <= 0)
                    {
                        LevelData.EnemyList.RemoveAt(i);
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
        }
        private void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("############################");
            Console.WriteLine("#                          #");
            Console.WriteLine("#        GAME OVER         #");
            Console.WriteLine("#                          #");
            Console.WriteLine("#   Your HP went below 0   #");
            Console.WriteLine("#                          #");
            Console.WriteLine("############################");
            Console.ReadKey();
            this.IsGameRunning = false;
        }
    }
}
