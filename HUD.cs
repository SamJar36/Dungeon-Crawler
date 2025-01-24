using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class HUD
    {
        private int HitPoints { get; set; }
        private int Level { get; set; }
        private ConsoleColor Color { get; set; }
        private ConsoleColor ColorLevel { get; set; }
        private ConsoleColor ColorSeparator { get; set; }
        private int Steps { get; set; }
        private int KillCount { get; set; }
        private int GoldCount { get; set; }
        private int KeyCount { get; set; }
        private int HealthPotionCount { get; set; }
        private string Weapon { get; set; }
        private string Armor { get; set; }

        public HUD(int hitPoints, int level, int steps, Dice weapon, Dice armor, int killCount, int goldCount, int keyCount, int healthPotionCount)
        {
            this.HitPoints = hitPoints;
            this.Level = level;
            //this.Steps = steps;
            //this.KillCount = killCount;
            this.GoldCount = goldCount;
            this.KeyCount = keyCount;
            this.HealthPotionCount = healthPotionCount;

            this.Weapon = $"{weapon.DiceName}({weapon.DiceNumber}d{weapon.DiceSides}+{weapon.DiceModifier})";
            this.Armor = $"{armor.DiceName}({armor.DiceNumber}d{armor.DiceSides}+{armor.DiceModifier})";

            this.Color = ConsoleColor.Yellow;
            this.ColorLevel = ConsoleColor.Green;
            this.ColorSeparator = ConsoleColor.White;
        }
        public void Draw(int HP, int steps, int killCount, int goldCount, int keyCount, int healthPotionCount, Dice weapon, Dice armor, int level)
        {
            this.HitPoints = HP;
            //this.Steps = steps;
            //this.KillCount = killCount;
            this.GoldCount = goldCount;
            this.KeyCount = keyCount;
            this.HealthPotionCount = healthPotionCount;
            this.Weapon = $"{weapon.DiceName}({weapon.DiceNumber}d{weapon.DiceSides}+{weapon.DiceModifier})";
            this.Armor = $"{armor.DiceName}({armor.DiceNumber}d{armor.DiceSides}+{armor.DiceModifier})";
            this.Level = level;

            Console.ForegroundColor = ColorLevel;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Level: {Level}");
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(104, 0);
            Console.WriteLine("X = \u001b[32mSAVE\u001b[0m/\u001b[31mQUIT\u001b[0m");
            Console.WriteLine(
                $"HP: {HitPoints}, " +
                $"Gold: {GoldCount}, " +
                //$"Steps: {Steps}, " +
                //$"Mobs killed: {KillCount}, " + 
                $"{Weapon}, " +
                $"{Armor}, " +
                $"Keys: {KeyCount}, " +
                $"Health Potions (D): {HealthPotionCount}");
            Console.ForegroundColor = ColorSeparator;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
        }
    }
}
