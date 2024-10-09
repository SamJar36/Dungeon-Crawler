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
                text = "a Small Key! It opens locked doors";
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
            else if (Contents == "mimic")
            {
                text = "a Mimic! It clamps down on you with its jaws and you can't move!";
                Mimic mimic = new Mimic(this.PosX, this.PosY, LData, Player);
                mimic.Draw();
                LData.LevelElementList.Add(mimic);
            }
            // fix this shit man
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