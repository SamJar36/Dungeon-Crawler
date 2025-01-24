using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler.Elements
{
    public class Mimic : Enemy
    {
        private bool IsActivated { get; set; }
        private bool IsFirstRoundOfBattle { get; set; }
        public Mimic(int x, int y, LevelData levelData, Player player)
            : base(x, y,
                  20,
                  'C',
                  "Mimic",
                  ConsoleColor.Cyan,
                  levelData,
                  player,
                  new int[] { 3, 6, 2 },
                  new int[] { 1, 6, 2 },
                  new List<LootItem> 
                    { new LootItem("heartpiece", 1),
                      new LootItem("gold", 2, 15, 26)
                    })
        {
            this.IsActivated = false;
            this.IsFirstRoundOfBattle = true;
        }
        public override void Update()
        {
            Draw();
            if (this.IsActivated)
            {   
                if (!this.IsFirstRoundOfBattle)
                {
                    if (this.IsAbleToMove)
                    {
                        Battle(Player);
                        CheckIfHitPointsBelowZero();
                    }
                    this.IsAbleToMove = true;
                }
                this.IsFirstRoundOfBattle = false;
            }   
        }
        public void ActivateMimic()
        {
            Player.IsAbleToMove = false;
            Player.SoundEffects.PlaySoundEffect("MimicStart");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("The treasure chest was actually a \x1b[31mMIMIC\x1b[0m! You're trapped in its jaws!");
            this.Symbol = 'M';
            this.Color = ConsoleColor.Red;
            Draw();

            this.IsActivated = true;
        }
    }
}
