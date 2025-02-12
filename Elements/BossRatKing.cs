﻿using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class BossRatKing : Enemy
    {
        
        public BossRatKing(int x, int y, LevelData levelData, Player player) 
            : base(x, y, 
                  25, 
                  'B', 
                  "Rat King",
                  ConsoleColor.Red, 
                  levelData, 
                  player,
                  new int[] { 2, 6, 3},
                  new int[] { 1, 6, 3},
                  new List<LootItem> 
                    { new LootItem("key", 1)
                    })
        {

        }
        public override void Update()
        {
            if (this.IsAbleToMove)
            {
                Move();
                EraseLastPositionOfEnemy();
            }
            Draw();
            CheckIfHitPointsBelowZero();
            this.IsAbleToMove = true;
        }
        public void Move()
        {
            Random random = new Random();
            int direction = random.Next(1, 5);
            LastPositionOfEnemy();
            if (direction == 1)
            {
                this.PosX -= 1;
                EnemyCheckForCollision();
            }
            else if (direction == 2)
            {
                this.PosX += 1;
                EnemyCheckForCollision();
            }
            else if (direction == 3)
            {
                this.PosY += 1;
                EnemyCheckForCollision();
            }
            else if (direction == 4)
            {
                this.PosY -= 1;
                EnemyCheckForCollision();
            }
        }
    }
}
