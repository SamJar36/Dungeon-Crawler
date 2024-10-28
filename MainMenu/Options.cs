using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.MainMenu
{
    internal class Options : MenuElement
    {
        public Options() : base(new List<string> { "Resolution", "Select Difficulty", "Fullscreen", "Color Scheme", "Fog of War", "Egg" }, "Options")
        {

        }
    }
}
