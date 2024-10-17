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
        private LevelData LData { get; set; }
        private HUD Hud { get; set; }
        private Equipment EQ { get; set; }
        public GameLoop(LevelData levelData, HUD hud, Equipment eQ)
        {
            this.LData = levelData;
            this.Hud = hud;
            this.IsGameRunning = true;
            this.EQ = eQ;
        }
        public void Run()
        {
            while (IsGameRunning)
            {
                if (LData.Player.HitPoints <= 0)
                {
                    GameOver();
                    continue;
                }
                Hud.Draw(
                    LData.Player.HitPoints, 
                    LData.Player.Steps, 
                    LData.Player.KillCount, 
                    LData.Player.GoldCount,
                    LData.Player.KeyCount,
                    LData.Player.HealthPotionCount,
                    LData.Player.EquippedWeapon,
                    LData.Player.EquippedArmor);
                LData.Player.Update();
                foreach (var enemy in LData.EnemyList)
                {
                    enemy.Update();
                }
                foreach (var element in LData.LevelElementList)
                {
                    element.Draw();
                }
                // can't remove an enemy from list within the foreach loop above
                for (int i = LData.EnemyList.Count - 1; i >= 0; i--)
                {
                    if (LData.EnemyList[i].HitPoints <= 0)
                    {
                        LData.EnemyList.RemoveAt(i);
                    }
                }
                if (LData.IsSwitchingLevels)
                {
                    SwitchLevel();
                }
                LData.IsSwitchingLevels = false;
                LData.Player.IsCurrentlyInABattle = false;
            }
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
        }
        private void SwitchLevel()
        {
            for (int i = LData.LevelElementList.Count - 1; i >= 0; i--)
            {
                LData.LevelElementList.RemoveAt(i);
            }
            for (int i = LData.EnemyList.Count - 1; i >= 0; i--)
            {
                LData.EnemyList.RemoveAt(i);
            }
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
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            LData.LoadMap(EQ);
            LData.Player.DrawPlayer();
        }
        private void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("##########################");
            Console.WriteLine("#                        #");
            Console.WriteLine("#       GAME OVER        #");
            Console.WriteLine("#                        #");
            Console.WriteLine("#  Your HP went below 0  #");
            Console.WriteLine("#                        #");
            Console.WriteLine("##########################");
            Thread.Sleep(2000);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            this.IsGameRunning = false;
        }
    }
}
