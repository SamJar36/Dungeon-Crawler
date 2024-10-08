using DungeonCrawler;
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
                  15,
                  's',
                  "snake",
                  ConsoleColor.Red,
                  levelData,
                  player,
                  new int[] { 3, 4, 2 },
                  new int[] { 1, 8, 1 })
        {

        }
        public override void Update()
        {
            Move();
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

                double magnitude = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
                directionX /= magnitude;
                directionY /= magnitude;

                this.PosX += (int)directionX;
                this.PosY += (int)directionY;

                EnemyCheckForCollision();
            }
        }
    }
}
