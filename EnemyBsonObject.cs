using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dungeon_Crawler.Elements;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dungeon_Crawler
{
    public class EnemyBsonObject
    {
        public List<Rat> Rats { get; set; }
        public List<Snake> Snakes { get; set; }
        public List<Mimic> Mimics { get; set; }
        public List<BossRatKing> BossRatKings { get; set; }
        public List<BossSkeletonLeader> BossSkeletonLeaders { get; set; }
        public EnemyBsonObject(List<Enemy> enemyList)
        {

            this.Rats = new List<Rat>();
            this.Snakes = new List<Snake>();
            this.Mimics = new List<Mimic>();
            this.BossSkeletonLeaders = new List<BossSkeletonLeader>();
            this.BossRatKings = new List<BossRatKing>();
            foreach (var enemy in enemyList)
            {
                if (enemy is Rat rat)
                {
                    this.Rats.Add(rat);
                }
                else if (enemy is Snake snake)
                {
                    this.Snakes.Add(snake);
                }
                else if (enemy is Mimic mimic)
                {
                    this.Mimics.Add(mimic);
                }
                else if (enemy is BossRatKing bossRatKing)
                {
                    this.BossRatKings.Add(bossRatKing);
                }
                else if (enemy is BossSkeletonLeader bossSkeletonLeaders)
                {
                    this.BossSkeletonLeaders.Add(bossSkeletonLeaders);
                }
            }
        }
        public EnemyBsonObject()
        {

        }
        public List<Enemy> CombineLists()
        {
            List<Enemy> enemyList = new List<Enemy>();
            return enemyList
                .Concat(Rats)
                .Concat(Snakes)
                .Concat(Mimics)
                .Concat(BossRatKings)
                .Concat(BossSkeletonLeaders)
                .ToList();
        }
    }
}
