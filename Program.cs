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
        HUD hud = new HUD(
            levelData.Player.HitPoints,
            levelData.Level, 
            levelData.Player.Steps,
            levelData.Player.EquippedWeapon,
            levelData.Player.EquippedArmor,
            levelData.Player.KillCount,
            levelData.Player.GoldCount,
            levelData.Player.KeyCount);

        //Attacks and Defense
        Equipment equipment = new Equipment();

        //Game Loop
        GameLoop gameLoop = new GameLoop(levelData, hud);

        //Run the game
        gameLoop.Run();
        
        //Things to fix

        // random gold depending on enemy
        // health potions (press D to drink)
        // locked doors and keys
        // treasure chests hold items (hard coded locations)
        // 
    }
}
