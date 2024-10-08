﻿using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            foreach (var element in LData.LevelElementList)
            { 
                if (element is Wall wall)
                {
                    IfMovementBlockedGoBack(wall);
                }
                if (element is LockedDoor door)
                {
                    IfMovementBlockedGoBack(door);
                }
                if (element is TreasureChest chest)
                {
                    IfMovementBlockedGoBack(chest);
                }
                if (element is Gold gold)
                {
                    IfMovementBlockedGoBack(gold);
                }
            }
            foreach (var enemy in LData.EnemyList)
            {
                if (enemy == this)
                {
                    continue;
                }
                IfMovementBlockedGoBack(enemy);
            }
            if (this.PosX == Player.PosX && this.PosY == Player.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;

                // start battle
            }
        }
        protected void Battle(Player player)
        { 
            int enemyAttack = this.AttackDice.Throw();
            int playerDefense = player.EquippedArmor.Throw();
            int result = enemyAttack - playerDefense;
            if (result < 0)
            {
                result = 0;
            }
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"The {this.Name} attacked you for {enemyAttack}, you defended for {playerDefense}. The {this.Name} dealt {result} damage!");
            player.HitPoints -= result;
            if (player.HitPoints > 0)
            {
                int playerAttack = player.EquippedWeapon.Throw();
                int enemyDefense = this.DefenseDice.Throw();
                result = playerAttack - enemyDefense;
                if (result < 0)
                {
                    result = 0;
                }
                Console.SetCursorPosition(0, 4);
                Console.WriteLine($"You attacked the {this.Name} for {playerAttack}, the {this.Name} defended for {enemyDefense}. The {this.Name} dealt {result} damage!");
                if (this.HitPoints < 1)
                {
                    Console.Write($" The {this.Name} is defeated!");
                    player.KillCount += 1;
                }
            }
            else
            {
                player.GameOver();
            }
        }
        public void CheckIfHitPointsBelowZero()
        {
            if (this.HitPoints <= 0)
            {
                if (this is Rat rat)
                {
                    Gold gold = new Gold(this.PosX, this.PosY, this.Player, 1, 3);
                    LData.LevelElementList.Add(gold);
                }
                else if (this is Snake snake)
                {
                    Gold gold = new Gold(this.PosX, this.PosY, this.Player, 2, 6);
                    LData.LevelElementList.Add(gold);
                }
                else if (this is Mimic mimic)
                {
                    Gold gold = new Gold(this.PosX, this.PosY, this.Player, 12, 26);
                    LData.LevelElementList.Add(gold);
                    Player.IsAbleToMove = true;
                }
            }   
        }
        public void IfMovementBlockedGoBack(LevelElement element)
        {
            if (this.PosX == element.PosX && this.PosY == element.PosY)
            {
                this.PosX = LastPosX;
                this.PosY = LastPosY;
            }
        }
    }
}