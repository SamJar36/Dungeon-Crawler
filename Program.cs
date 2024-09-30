using Dungeon_Crawler.Elements;

namespace DungeonCrawler;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        bool isGameRunning = true;

        //map
        LevelData levelData = new LevelData();
        levelData.LoadMap();

        //player
        Player player = new Player(10, 10);

        while (isGameRunning)
        {
            player.MovePlayer();
            player.DrawPlayer(player.PosX, player.PosY);
        }
        Console.Clear();
        Console.WriteLine("Thanks for playing!");
    }
}
