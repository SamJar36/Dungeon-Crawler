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
        public string Name { get; set; }
        private int LastPosX { get; set; }
        private int LastPosY { get; set; }
        protected LevelData LData { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }
        public Enemy(int x, int y, int HP, char symbol, string name, ConsoleColor color, LevelData levelData, Player player, int[] attackArray, int[] defenseArray) 
            : base(x, y, symbol, color, player)
        {
            this.HitPoints = HP;
            this.Name = name;

            this.LastPosX = x;
            this.LastPosY = y;

            this.LData = levelData;

            this.AttackDice = new Dice(attackArray[0], attackArray[1], attackArray[2]);
            this.DefenseDice = new Dice(defenseArray[0], defenseArray[1], defenseArray[2]);
        }
        public abstract void Update();
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
            foreach (var enemy in LData.EnemyList)
            {
                if (enemy == this)
                {
                    continue;
                }
                if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
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
        public void CheckIfHitPointsBelowZero()
        {
            if (this.HitPoints <= 0)
            {
                Gold gold = new Gold(this.PosX, this.PosY, this.Player);
                LData.LevelElementList.Add(gold);
            }   
        }
    }
}