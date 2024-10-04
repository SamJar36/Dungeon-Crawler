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
        private Random random = new Random();
        public Rat(int x, int y, LevelData levelData) : base(x, y, 5, 'r', ConsoleColor.Red, levelData)
        {

        }
        public void Move()
        {
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
