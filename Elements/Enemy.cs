using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace Dungeon_Crawler.Elements
{
    public abstract class Enemy : LevelElement
    {
        public int HitPoints { get; set; }
        public string Name { get; set; }
        private int LastPosX { get; set; }
        private int LastPosY { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenseDice { get; set; }
        public bool IsAbleToMove { get; set; }
        [BsonIgnore]
        public LevelData LData { get; set; }
        public List<LootItem> LootTable { get; set; }
        public Enemy(int x, 
                     int y, 
                     int HP, 
                     char symbol, 
                     string name, 
                     ConsoleColor color, 
                     LevelData levelData, 
                     Player player, 
                     int[] attackArray, 
                     int[] defenseArray, 
                     List<LootItem> lootTable) 

                     : base(x, y, symbol, color, player)
        {
            this.HitPoints = HP;
            this.Name = name;
            this.LastPosX = x;
            this.LastPosY = y;
            this.LData = levelData;
            this.AttackDice = new Dice(attackArray[0], attackArray[1], attackArray[2]);
            this.DefenseDice = new Dice(defenseArray[0], defenseArray[1], defenseArray[2]);
            this.IsAbleToMove = true;
            this.LootTable = lootTable;
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
                if (element == this)
                {
                    continue;
                }
                if (this.PosX == element.PosX && this.PosY == element.PosY)
                {
                    if (element is Wall wall)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is LockedDoor door)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is TreasureChest chest)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is Gold gold)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is MagicalBarrier barrier)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is MagicalKey magicalKey)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is HeartPiece heart)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is FakeWall fakeWall)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is HiddenWall hiddenWall)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is Warp warp)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is Key key)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is ShopKeeper shopKeeper)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is ArrowTile arrow)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is GreenWall gwall)
                    {
                        MovementIsBlockedGoBack();
                    }
                    else if (element is PushableBlock pBlock)
                    {
                        MovementIsBlockedGoBack();
                    }
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
                        MovementIsBlockedGoBack();
                    }
            }
            if (this.PosX == Player.PosX && this.PosY == Player.PosY)
            {
                MovementIsBlockedGoBack();

                if (!Player.IsCurrentlyInABattle)
                {
                    Battle(Player);
                }
            }
        }
        protected void Battle(Player player)
        {
            Random random = new Random();
            if (this is Mimic)
            {
                Player.SoundEffects.PlaySoundEffect("MimicAttack");
            }
            else
            {
                int battleNoiseChance = random.Next(1, 3);
                if (battleNoiseChance == 1)
                {
                    Player.SoundEffects.PlaySoundEffect("Fight1");
                }
                else if (battleNoiseChance == 2)
                {
                    Player.SoundEffects.PlaySoundEffect("Fight2");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            int enemyAttack = this.AttackDice.Throw();
            int playerDefense = player.EquippedArmor.Throw();
            int result = enemyAttack - playerDefense;
            if (result < 0)
            {
                result = 0;
            }
            else if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

            }
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"The {this.Name} attacked you for {enemyAttack}, you defended for {playerDefense}. The {this.Name} dealt {result} damage!");
            player.HitPoints -= result;
            if (player.HitPoints > 0)
            {
                int playerAttack = player.EquippedWeapon.Throw();
                int enemyDefense = this.DefenseDice.Throw();
                result = playerAttack - enemyDefense;
                if (result <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    result = 0;
                }
                else if (result > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                }
                Console.SetCursorPosition(0, 4);
                this.HitPoints -= result;
                string enemyDefeatedText = "";
                if (this.HitPoints < 1)
                {
                    enemyDefeatedText = $"The {this.Name} is defeated!";
                    player.KillCount += 1;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"You attacked the {this.Name} for {playerAttack}, the {this.Name} defended for {enemyDefense}. You dealt {result} damage! {enemyDefeatedText}");
            }
        }
        public void CheckIfHitPointsBelowZero()
        {
            if (this.HitPoints <= 0)
            {
                if (this is Mimic mimic)
                {
                    this.Player.IsAbleToMove = true;
                }
                CreateLoot();
            }   
        }
        public void CreateLoot()
        {
            Random lootDropChance = new Random();
            int maxValue = LootTable.Sum(obj => obj.Odds);
            int chosenValue = lootDropChance.Next(0, maxValue);
            int currentValue = 0;
            for (int i = 0; i < this.LootTable.Count; i++)
            {
                currentValue += this.LootTable[i].Odds;
                if (currentValue >= chosenValue)
                {
                    if (this.LootTable[i].Name == "gold")
                    {
                        Gold gold = new Gold(this.PosX, this.PosY, Player, this.LootTable[i].MinValue, this.LootTable[i].MaxValue);
                        LData.LevelElementList.Add(gold);
                    }
                    else if (this.LootTable[i].Name == "heartpiece")
                    {
                        HeartPiece heart = new HeartPiece(this.PosX, this.PosY, Player);
                        LData.LevelElementList.Add(heart);
                    }
                    else if (this.LootTable[i].Name == "key")
                    {
                        Key key = new Key(this.PosX, this.PosY, Player);
                        LData.LevelElementList.Add(key);
                    }
                    break;
                }
            }
        }
        public void MovementIsBlockedGoBack()
        {
            this.PosX = LastPosX;
            this.PosY = LastPosY;
        }
    }
}