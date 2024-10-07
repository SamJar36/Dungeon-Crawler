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

        public Dice Tunic { get; set; }

        public Equipment()
        {
            // Weapons
            Dice woodenSword = new Dice(1, 6, 2, "Wooden Sword");
            this.WoodenSword = woodenSword;

            // Armor
            Dice tunic = new Dice(1, 6, 0, "Tunic");
            this.Tunic = tunic;
    
        }
    }
}
