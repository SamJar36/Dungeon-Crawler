using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
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

        // redo loot system
        // remove yOffset in levelData
        // remove the ints "attackplaceinHud" or whatever they're called
        // add chests on second level
        // guard enemy (stationary)
        // heart bool on object creation that determines if a heart is red or gold (so it doesn't randomize when created in map level)
    }
}
