using Dungeon_Crawler;
using Dungeon_Crawler.Elements;
using Dungeon_Crawler.MainMenu;
using System.Security.Cryptography.X509Certificates;
using NAudio.Wave;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DungeonCrawler;

public class Program
{
    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOMOVE = 0x0002;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_FRAMECHANGED = 0x0020;
    private const int GWL_STYLE = -16;
    private const int WS_SIZEBOX = 0x00040000;
    private const int WS_MAXIMIZEBOX = 0x00010000;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    static void Main(string[] args)
    {
        IntPtr consoleWindow = GetConsoleWindow();
        if (consoleWindow == IntPtr.Zero)
        {
            Console.WriteLine("Unable to get console window handle.");
            return;
        }

        int style = GetWindowLong(consoleWindow, GWL_STYLE);
        style &= ~WS_SIZEBOX; // Remove the resize border
        style &= ~WS_MAXIMIZEBOX; // Remove the maximize button
        SetWindowLong(consoleWindow, GWL_STYLE, style);
        SetWindowPos(consoleWindow, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);

        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..");
        Directory.SetCurrentDirectory(projectDirectory);

        //Equipment
        Equipment EQ = new Equipment();

        GameDataService gameDataService = new GameDataService("mongodb://localhost:27017/", "GameData");
        LevelData levelData = new LevelData(gameDataService);

        ////Main Menu
        MainMenu mainMenu = new MainMenu();
        bool menuLoop = true;
        bool IsPickingNewGame = true;
        Music music = new Music();
        music.PlayMusic("TitleScreen");
        while (menuLoop)
        {
            mainMenu.Draw();
            var keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.DownArrow && mainMenu.arrowDrawIndex < 2)
            {
                mainMenu.arrowDrawIndex++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && mainMenu.arrowDrawIndex > 0)
            {
                mainMenu.arrowDrawIndex--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                if (mainMenu.arrowDrawIndex == 0)
                {
                    menuLoop = false;
                    Console.Clear();   
                }
                else if (mainMenu.arrowDrawIndex == 1)
                {
                    bool isDataPresent = gameDataService.IsThereSavedDataInCollections();
                    if (isDataPresent)
                    {
                        menuLoop = false;
                        Console.Clear();
                        IsPickingNewGame = false;
                    }
                    else
                    {
                        Console.Clear();
                        int menuHeight = 3;
                        int menuWidth = 37 + 5;
                        int setCursorX = Console.WindowWidth / 2 - menuWidth / 2;
                        int setCursorY = Console.WindowHeight / 2 - (menuHeight / 2) - 1;
                        Console.SetCursorPosition(setCursorX, setCursorY);
                        Console.WriteLine("#####################################");
                        Console.SetCursorPosition(setCursorX, setCursorY + 1);
                        Console.WriteLine("#   Error! No saved data detected   #");
                        Console.SetCursorPosition(setCursorX, setCursorY + 2);
                        Console.WriteLine("#####################################");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }     
                }
                else if (mainMenu.arrowDrawIndex == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Exiting game. Thank you for playing!");
                    Environment.Exit(1);
                }
            }
            Console.Clear();
        }
        music.StopMusic();
        //Map
        if (IsPickingNewGame)
        {
            levelData.LoadMapFromText();
        }
        else
        {
            gameDataService.LoadSavedMap(levelData);
        }

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
        // warp on same vertical plane bug (game crashes)
        // Warping and then attacking results in "warp exit is blocked message"

        // pushable blocks after quickload
        // all walls after loads aren't properly discovered

    }
}
