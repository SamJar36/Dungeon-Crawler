using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class Dice
    {
        public string DiceName { get; set; }
        public int DiceNumber { get; set; }
        public int DiceSides { get; set; }
        public int DiceModifier { get; set; }
        private Random random = new Random();
        public Dice(int diceNumber, int diceSides, int diceModifier)
        {
            this.DiceNumber = diceNumber;
            this.DiceSides = diceSides;
            this.DiceModifier = diceModifier;
        }
        public Dice(int diceNumber, int diceSides, int diceModifier, string name) 
            : this(diceNumber, diceSides, diceModifier)
        {
            this.DiceName = name;
        }
        public int Throw()
        {
            int total = 0;
            int diceResult = 0;
            for (int i = 1; i <= DiceNumber; i++)
            {
                diceResult = random.Next(1, DiceSides);
                total += diceResult;
            }
            total += DiceModifier;
            return total;
        }
    }
}
