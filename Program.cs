using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using System.Security.Cryptography.X509Certificates;

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
            levelData.CurrentLevel, 
            levelData.Player.Steps,
            levelData.Player.EquippedWeapon,
            levelData.Player.EquippedArmor,
            levelData.Player.KillCount,
            levelData.Player.GoldCount,
            levelData.Player.KeyCount,
            levelData.Player.HealthPotionCount);

        //Game Loop
        GameLoop gameLoop = new GameLoop(levelData, hud);

        //Run the game
        gameLoop.Run();

        // redo loot system
    }
}
