using DungeonCrawler;

namespace Dungeon_Crawler.Elements;

public class TreasureChest : LevelElement
{
    private string Contents { get; set; }
    private LevelData LData { get; set; }
    private Player Player { get; set; }
    public TreasureChest(int x, int y, Player player, string contents, LevelData levelData) : base(x, y, 'C', ConsoleColor.Cyan, player)
    {
        this.Contents = contents;
        this.LData = levelData;
        this.Player = player;
    }
    public void OpenTreasureChest()
    {
        if (IsDrawing)
        {
            string text = "";
            if (Contents == "key")
            {
                this.Player.KeyCount += 1;
                text = "a Small Key!";
            }
            else if (Contents == "health potion")
            {
                this.Player.HealthPotionCount += 1;
                text = "a Health Potion! It recovers 20-50 HP";
            }
            else if (Contents == "empty")
            {
                text = "... nothing";
            }
            Console.SetCursorPosition(0, 3);
            Console.Write($"You opened the chest and found {text}");
            this.Player.PosX = this.Player.LastPosX;
            this.Player.PosY = this.Player.LastPosY;
            this.Player.Steps--;

            this.Symbol = ' ';
            this.Draw();
            this.IsDrawing = false;
        }
    }
}