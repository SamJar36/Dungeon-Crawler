using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.MainMenu
{
    internal class Difficulty : MenuElement
    {
        public Difficulty() : base(new List<string> { "Easy", "Medium", "Hard", "Super-Extra-Ordinarily-Duper-Hard", "Back" }, "Select Difficulty")
        {

        }
    }
}
