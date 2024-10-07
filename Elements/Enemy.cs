using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public abstract class Enemy : LevelElement
    {
        public int HitPoints { get; set; }
        private int LastPosX { get; set; }
        private int LastPosY { get; set; }
        private LevelData LData { get; set; }
        private Player Player { get; set; }
        public Enemy(int x, int y, int HP, char symbol, ConsoleColor color, LevelData levelData, Player player) : base(x, y, symbol, color)
        {
            this.HitPoints = HP;

            this.LastPosX = x;
            this.LastPosY = y;

            this.LData = levelData;

            this.Player = player;
        }
        public void LastPositionOfEnemy()
        {
            this.LastPosX = this.PosX;
            this.LastPosY = this.PosY;
        }
        public void EraseLastPositionOfEnemy()
        {
            Console.SetCursorPosition(this.LastPosX, this.LastPosY);
            Console.Write(" ");
        }
        public void EnemyCheckForCollision()
        {
            foreach (var item in LData.WallList)
            {
                if (this.PosX == item.PosX && this.PosY == item.PosY)
                {
                    this.PosX = LastPosX;
                    this.PosY = LastPosY;
                }
            }
            foreach (var rat in LData.RatList)
            {
                if (rat == this)
                {
                    continue;
                }
                if (this.PosX == rat.PosX && this.PosY == rat.PosY)
                {
                    this.PosX = LastPosX;
                    this.PosY = LastPosY;
                }
            }
            if (this.PosX == Player.PosX && this.PosY == Player.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;

                // start battle
            }
        }
    }
}
