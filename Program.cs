using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using Dungeon_Crawler.MainMenu;
using System.Security.Cryptography.X509Certificates;

namespace DungeonCrawler;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //Equipment
        Equipment EQ = new Equipment();

        //Main Menu
        MainMenu mainMenu = new MainMenu();
        Difficulty difficulty = new Difficulty();
        Options options = new Options();
        mainMenu.Draw();
        difficulty.Draw();
        options.Draw();


        //Map
        LevelData levelData = new LevelData();
        levelData.LoadMap(EQ);

        //HUD
        HUD hud = new HUD(
            levelData.Player.HitPoints,
            levelData.CurrentLevel, 
            levelData.Player.Steps,
            levelData.Player.EquippedWeapon,
            levelData.Player.EquippedArmor,
            levelData.Player.KillCount,
            levelData.Player.GoldCount,
            levelData.Player.KeyCount,
            levelData.Player.HealthPotionCount);

        

        //Game Loop
        GameLoop gameLoop = new GameLoop(levelData, hud, EQ);

        //Run the game
        gameLoop.Run();

        // remove yOffset in levelData
        // remove the ints "attackplaceinHud" or whatever they're called
        // guard enemy (stationary)
        // level2 boss
    }
}
