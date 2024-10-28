using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.MainMenu
{
    internal class MainMenu : MenuElement
    {
        public MainMenu() : base(new List<string> { "New Game", "Password", "Options", "About", "Exit" }, "Main Menu")
        {

        }
    }
}
