﻿using Dungeon_Crawler.Elements;
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
                // can't remove an item from list within the foreach loop above
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
    }
}
