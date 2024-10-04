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
        Player player = new Player(10, 10, levelData);

        while (isGameRunning)
        {
            player.MovePlayer(levelData);
            player.EraseLastPositionOfPlayer();
            player.DrawPlayer(player.PosX, player.PosY);
            foreach (var rat in levelData.RatList)
            {
                rat.Move();
                rat.EraseLastPositionOfEnemy();
                rat.Draw();  
            }

        }
        Console.Clear();
        Console.WriteLine("Thanks for playing!");
    }
}
