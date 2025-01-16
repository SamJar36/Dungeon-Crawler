using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.MainMenu
{
    internal abstract class MenuElement
    {
        protected string arrow = "--->";
        protected int MenuWidth { get; set; }
        protected int MenuHeight { get; set; }
        protected int SetCursorX { get; set; }
        protected int SetCursorY { get; set; }
        protected string Name { get; set; }
        public List<string> MenuItems { get; set; }
        public int arrowDrawIndex = 0;
        public MenuElement(List<string> menuItems, string name)
        {
            MenuHeight = menuItems.Count + 3;
            int longestMenuItemString = 0;
            foreach (string item in menuItems)
            {
                if (item.Length > longestMenuItemString)
                {
                    longestMenuItemString = item.Length;
                }
            }
            MenuWidth = longestMenuItemString + arrow.Length + 5;
            SetCursorX = Console.WindowWidth / 2 - MenuWidth / 2;
            SetCursorY = Console.WindowHeight / 2 - (MenuHeight / 2) - 1;
            Name = name;
            MenuItems = menuItems;
        }
        public MenuElement(int width, int height, string name)
        {
            this.MenuWidth = width;
            this.MenuHeight = height;
            this.Name = name;
        }
        public void Draw()
        {
            DrawBorders();
            DrawMenuItems();
            DrawArrow();
        }
        public void DrawBorders()
        {
            for (int j = 0; j <= MenuHeight; j++)
            {
                Console.SetCursorPosition(SetCursorX, SetCursorY + j);
                if (j > 0 && j < MenuHeight)
                {
                    Console.Write("#");
                    Console.SetCursorPosition(SetCursorX + MenuWidth, SetCursorY + j);
                    Console.Write("#");
                }
                else
                {
                    for (int i = 0; i <= MenuWidth; i++)
                    {
                        Console.Write("#");
                    }
                }
                Console.Write("\n");
            }         
        }
        public void DrawMenuItems()
        {
            int menuItemToDraw = 0;
            for (int i = 2; i < MenuHeight; i++)
            {
                if (menuItemToDraw != MenuItems.Count)
                {
                    Console.SetCursorPosition(SetCursorX + arrow.Length + 3, SetCursorY + i);
                    Console.Write($"{MenuItems[menuItemToDraw]}");
                    menuItemToDraw++;
                }
            }  
        }
        public void DrawArrow()
        {
            Console.SetCursorPosition(SetCursorX + 2, SetCursorY + 2 + (arrowDrawIndex));
            foreach (char c in arrow)
            {
                Console.Write(c);
            }
        }
    }
}
