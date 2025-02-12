﻿using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Snake : Enemy
    {
        public Snake(int x, int y, LevelData levelData, Player player)
            : base(x, y,
                  13,
                  's',
                  "snake",
                  ConsoleColor.Red,
                  levelData,
                  player,
                  new int[] { 3, 4, 2 },
                  new int[] { 1, 8, 1 },
                  new List<LootItem>
                    { new LootItem("heartpiece", 4),
                      new LootItem("gold", 6, 3, 7)
                    })
        {

        }
        public override void Update()
        {
            if (IsAbleToMove)
            {
                Move();
            }
            IsAbleToMove = true;
            EraseLastPositionOfEnemy();
            Draw();
            CheckIfHitPointsBelowZero();
        }
        public void Move()
        {
            double distance = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
            LastPositionOfEnemy();
            if (distance <= 2)
            {
                double directionX = this.PosX - this.Player.PosX;
                double directionY = this.PosY - this.Player.PosY;

                double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
                directionX /= magnitude;
                directionY /= magnitude;

                this.PosX += (int)Math.Round(directionX);
                this.PosY += (int)Math.Round(directionY);

                EnemyCheckForCollision();
            }
        }
    }
}
