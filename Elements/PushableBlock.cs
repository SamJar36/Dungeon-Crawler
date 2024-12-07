using DungeonCrawler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    internal class PushableBlock : LevelElement
    {
        private int LastPosX { get; set; }
        private int LastPosY { get; set; }
        public PushableBlock(int x, int y, Player player) : base(x, y, '#', ConsoleColor.DarkMagenta, player)
        {
            this.LastPosX = x;
            this.LastPosY = y;
        }
        public void Push(LevelData LData)
        {
            double distance = CalculateEuclideanDistance(this.PosX, this.PosY, this.Player.PosX, this.Player.PosY);
            LastPositionOfBlock();
            if (distance <= 1)
            {
                double directionX = this.PosX - this.Player.PosX;
                double directionY = this.PosY - this.Player.PosY;

                double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
                directionX /= magnitude;
                directionY /= magnitude;

                this.PosX += (int)Math.Round(directionX);
                this.PosY += (int)Math.Round(directionY);

                Player.SoundEffects.PlaySoundEffect("PushBlock");
                BlockCheckForCollision(LData);
                this.Draw();
                EraseLastPositionOfBlock();

            }
        }
        private void LastPositionOfBlock()
        {
            this.LastPosX = this.PosX;
            this.LastPosY = this.PosY;
        }
        private void EraseLastPositionOfBlock()
        {
            Console.SetCursorPosition(this.LastPosX, this.LastPosY);
            Console.Write(" ");
        }
        private void BlockCheckForCollision(LevelData LData)
        {
            foreach (var element in LData.LevelElementList)
            {
                if (element == this)
                {
                    continue;
                }
                else
                {
                    if (this.PosX == element.PosX && this.PosY == element.PosY)
                    {
                        MovementIsBlockedGoBack();
                    }
                }
            }
            foreach (var enemy in LData.EnemyList)
            {
                if (this.PosX == enemy.PosX && this.PosY == enemy.PosY)
                {
                    MovementIsBlockedGoBack();
                }
            }
        }
        private void MovementIsBlockedGoBack()
        {
            this.PosX = this.LastPosX;
            this.PosY = this.LastPosY;
        }
    }
}
