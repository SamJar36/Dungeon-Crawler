using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class Equipment
    {
        public Dice WoodenSword { get; set; }
        public Dice ShortSword { get; set; }
        public Dice DebuggingSword { get; set; }

        public Dice Tunic { get; set; }
        public Dice LeatherArmor { get; set; }

        public Equipment()
        {
            // Weapons
            Dice woodenSword = new Dice(1, 6, 4, "Wooden Sword");
            Dice shortSword = new Dice(2, 6, 2, "Short Sword");
            Dice debuggingSword = new Dice(3, 6, 5, "Debugging Sword");
            this.WoodenSword = woodenSword;
            this.ShortSword = shortSword;
            this.DebuggingSword = debuggingSword;

            // Armor
            Dice tunic = new Dice(1, 6, 0, "Tunic");
            Dice leatherArmor = new Dice(1, 8, 1, "Leather Armor");
            this.Tunic = tunic;
            this.LeatherArmor = leatherArmor;
    
        }
    }
}
