using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class BossSkeletonLeader : Enemy
    {
        private Random random = new Random();

        public BossSkeletonLeader(int x, int y, LevelData LData, Player player) 
            : base(x, y, 
                  25, 
                  'B', 
                  "Skeleton Leader",
                  ConsoleColor.Red,
                  LData,
                  player,
                  new int[] { 2, 6, 5},
                  new int[] { 1, 6, 1},
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
            double distance = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
            LastPositionOfEnemy();
            if (distance <= 3)
            {
                double directionX = this.PosX - this.Player.PosX;
                double directionY = this.PosY - this.Player.PosY;

                double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
                directionX /= magnitude;
                directionY /= magnitude;

                this.PosX -= (int)Math.Round(directionX);
                this.PosY -= (int)Math.Round(directionY);

                EnemyCheckForCollision();
            }
        }
    }
}
