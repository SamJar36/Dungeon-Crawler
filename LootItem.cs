using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler
{
    public class LootItem
    {
        public string Name { get; }
        public int Odds { get; }
        public int MinValue { get; }
        public int MaxValue { get; }

        public LootItem(string name, int odds)
        {
            Name = name;
            Odds = odds;
        }
        public LootItem(string name, int odds, int minValue, int maxValue) : this(name, odds)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
