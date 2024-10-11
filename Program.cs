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
            levelData.Level, 
            levelData.Player.Steps,
            levelData.Player.EquippedWeapon,
            levelData.Player.EquippedArmor,
            levelData.Player.KillCount,
            levelData.Player.GoldCount,
            levelData.Player.KeyCount,
            levelData.Player.HealthPotionCount);

        //Attacks and Defense
        Equipment equipment = new Equipment();

        //Game Loop
        GameLoop gameLoop = new GameLoop(levelData, hud);

        //Run the game
        gameLoop.Run();
        
        //Things to fix

        // when enemy attacks, enemy defeat message is in a weird location
        // random loot (gold, health potions, health, weapons and armor)
        // gain a magical spell that lets you break the seal in order to move to next level
        // only 1 enemy battle per round
        // game over screen
    }
}
