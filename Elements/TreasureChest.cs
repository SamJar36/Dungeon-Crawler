using DungeonCrawler;
using Newtonsoft.Json;

namespace Dungeon_Crawler.Elements;

public class TreasureChest : LevelElement
{
    public string Contents { get; set; }
    public Equipment EQ { get; set; }
    public TreasureChest(int x, int y, Player player, string contents) : base(x, y, 'C', ConsoleColor.Cyan, player)
    {
        this.Contents = contents;
        this.EQ = new Equipment();
    }
    public void OpenTreasureChest()
    {
        if (IsDrawing)
        {
            string text = "";
            if (Contents == "key")
            {
                this.Player.SoundEffects.PlaySoundEffect("Key");
                this.Player.KeyCount += 1;
                text = "a Small Key!";
            }
            else if (Contents == "health potion")
            {
                this.Player.HealthPotionCount += 1;
                this.Player.SoundEffects.PlaySoundEffect("Key");
                text = "a \u001b[32mHealth Potion\u001b[0m! It recovers \u001b[32m75-100\u001b[0m HP";
            }
            else if (Contents == "empty")
            {
                Player.SoundEffects.PlaySoundEffect("EmptyChest");
                text = "... nothing";
            }
            else if (Contents == "gold")
            {
                text = "10 gold";
                Player.SoundEffects.PlaySoundEffect("Coins");
            }
            else if (Contents == "leather armor")
            {
                text = "a Leather Armor! (1d8+1)";
                this.Player.EquippedArmor = EQ.LeatherArmor;
            }
            else if (Contents == "short sword")
            {
                text = "a Short Sword! (2d6+2)";
                this.Player.EquippedWeapon = EQ.ShortSword;
            }
            Console.SetCursorPosition(0, 3);
            Console.Write($"You opened the chest and found {text}");

            this.Player.MovementIsBlockedGoBack();

            this.Symbol = ' ';
            this.Draw();
            this.IsDrawing = false;
        }
    }
}