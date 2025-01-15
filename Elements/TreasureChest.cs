using DungeonCrawler;
using Newtonsoft.Json;

namespace Dungeon_Crawler.Elements;

public class TreasureChest : LevelElement
{
    public string Contents { get; set; }
    private LevelData LData { get; set; }
    [JsonIgnore]
    public Player Player { get; set; }
    [JsonIgnore]
    public Equipment EQ { get; set; }
    public TreasureChest(int x, int y, Player player, string contents, LevelData levelData, Equipment eQ) : base(x, y, 'C', ConsoleColor.Cyan, player)
    {
        this.Contents = contents;
        this.LData = levelData;
        this.Player = player;
        this.EQ = eQ;
    }
    public void OpenTreasureChest()
    {
        if (IsDrawing)
        {
            string text = "";
            if (Contents == "key")
            {
                Player.SoundEffects.PlaySoundEffect("Key");
                this.Player.KeyCount += 1;
                text = "a Small Key!";
            }
            else if (Contents == "health potion")
            {
                this.Player.HealthPotionCount += 1;
                text = "a Health Potion! It recovers 75-100 HP";
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