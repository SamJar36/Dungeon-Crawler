using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class Dice
    {
        private int DiceNumber { get; set; }
        private int DiceSides { get; set; }
        private int DiceModifier { get; set; }
        private Random random = new Random();
        public Dice(int diceNumber, int diceSides, int diceModifier)
        {
            this.DiceNumber = diceNumber;
            this.DiceSides = diceSides;
            this.DiceModifier = diceModifier;
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
