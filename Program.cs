namespace DungeonCrawler;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        bool isGameRunning = true;

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
