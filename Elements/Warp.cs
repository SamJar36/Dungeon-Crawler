using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Warp : LevelElement
    {
        private string FilePath { get; set; }
        private int WarpExitX { get; set; }
        private int WarpExitY { get; set; }
        private int WarpEntryCharacter { get; set; }

        public Warp(int x, int y, Player player, string filePath, int warpEntryCharacter) : base(x, y, 'W', ConsoleColor.Green, player)
        {
            this.FilePath = filePath;
            this.WarpEntryCharacter = warpEntryCharacter;
            SetWarpPoint();

        }
        public void SetWarpPoint()
        {
            List<string> mapLines = new List<string>();
            mapLines.Clear();
            string line;
            using (StreamReader lookingThroughLevelForOtherWarpPoint = new StreamReader(FilePath))
            {
                while ((line = lookingThroughLevelForOtherWarpPoint.ReadLine()) != null)
                {
                    mapLines.Add(line);
                }

            }
            for (int mapY = 0; mapY < mapLines.Count; mapY++)
            {
                for (int mapX = 0; mapX < mapLines[mapY].Length; mapX++)
                {
                    if (mapLines[mapY][mapX] == WarpEntryCharacter && (mapX != this.PosX && mapY != this.PosY))
                    {
                        char above = mapY > 0 ? mapLines[mapY - 1][mapX] : '\0';
                        char right = mapX < mapLines[mapY].Length - 1 ? mapLines[mapY][mapX + 1] : '\0';
                        char below = mapY < mapLines.Count - 1 ? mapLines[mapY + 1][mapX] : '\0';
                        char left = mapX > 0 ? mapLines[mapY][mapX - 1] : '\0';

                        if (above == ',')
                        {
                            WarpExitX = mapX;
                            WarpExitY = mapY - 1 + 5;
                        }
                        else if (right == ',')
                        {
                            WarpExitX = mapX + 1;
                            WarpExitY = mapY + 5;
                        }
                        else if (below == ',')
                        {
                            WarpExitX = mapX;
                            WarpExitY = mapY + 1 + 5;
                        }
                        else if (left == ',')
                        {
                            WarpExitX = mapX - 1;
                            WarpExitY = mapY + 5;
                        }
                        return;
                    } 
                }
            }
        }
        public void UseWarp(int currentlevel)
        {
            this.Player.PosX = this.WarpExitX;
            this.Player.PosY = this.WarpExitY;
        }
    }
}
