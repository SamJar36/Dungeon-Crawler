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

        while (isGameRunning)
        {
            levelData.Player.MovePlayer();
            levelData.Player.EraseLastPositionOfPlayer();
            levelData.Player.DrawPlayer(levelData.Player.PosX, levelData.Player.PosY);
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
