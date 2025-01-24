using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Rat : Enemy
    {

        public Rat(int x, int y, LevelData levelData, Player player) 
            : base(x, y, 
                  10, 
                  'r', 
                  "rat",
                  ConsoleColor.Red, 
                  levelData, 
                  player,
                  new int[] { 1, 6, 5},
                  new int[] { 1, 6, 0},
                  new List<LootItem>
                    { new LootItem("gold", 8, 1, 3),
                      new LootItem("heartpiece", 2)
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
