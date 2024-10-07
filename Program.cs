using Dungeon_Crawler;
using Dungeon_Crawler.Elements;

namespace DungeonCrawler;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        //Map
        LevelData levelData = new LevelData();
        levelData.LoadMap();

        //HUD
        HUD hud = new HUD(levelData.Player.HitPoints, levelData.Level, levelData.Player.Steps, levelData.Player.EquippedWeapon, levelData.Player.EquippedArmor);

        //Attacks and Defense
        Equipment equipment = new Equipment();

        //Game Loop
        GameLoop gameLoop = new GameLoop(levelData, hud);

        //Run the game
        gameLoop.Run();
        
        //Things to fix
        // -1 in battle damage
        // enemy disappears when dead
        // enemy update instead
        // enemy list
    }
}
