using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Mimic : Enemy
    {
        public Mimic(int x, int y, LevelData levelData, Player player)
            : base(x, y,
                  20,
                  'M',
                  "Mimic",
                  ConsoleColor.Red,
                  levelData,
                  player,
                  new int[] { 3, 6, 2 },
                  new int[] { 1, 6, 2 })
        {

        }
        public override void Update()
        {
            Player.IsAbleToMove = false;
            AttackPlayer();
            Draw();
            CheckIfHitPointsBelowZero();   
        }
        public void AttackPlayer()
        {
            Battle(Player);
        }
    }
}
