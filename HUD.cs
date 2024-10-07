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
        private string Weapon { get; set; }
        private string Armor { get; set; }

        public HUD(int hitPoints, int level, int steps, Dice weapon, Dice armor)
        {
            this.HitPoints = hitPoints;
            this.Level = level;
            this.Steps = steps;

            this.Weapon = $"{weapon.DiceName}({weapon.DiceNumber}d{weapon.DiceSides}+{weapon.DiceModifier})";
            this.Armor = $"{armor.DiceName}({armor.DiceNumber}d{armor.DiceSides}+{armor.DiceModifier})";

            this.Color = ConsoleColor.Yellow;
            this.ColorLevel = ConsoleColor.Green;
            this.ColorSeparator = ConsoleColor.White;
        }
        public void Draw(int HP, int steps)
        {
            this.HitPoints = HP;
            this.Steps = steps;
            
            Console.ForegroundColor = ColorLevel;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Level: {Level}");
            Console.ForegroundColor = Color;
            Console.WriteLine($"HP: {HitPoints}, Steps: {Steps}, {Weapon}, {Armor}");
            Console.ForegroundColor = ColorSeparator;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
        }
    }
}
