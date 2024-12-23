using System;
using Stars_Forsaken.Utilities.DirFilEdit;
using Stars_Forsaken.Config.Models;
using Stars_Forsaken.Config.Models.enums;
using Stars_Forsaken.Utilities.ConfigLoader;
using Stars_Forsaken.Logging;
using Stars_Forsaken;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Serilog;
using System.Threading;
using System.Runtime.InteropServices;
using Stars_Forsaken.Utilities.ConsoleInterpreter;

namespace Stars_Forsaken;
public class Program
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    static void Main(string[] args)
    {
        Thread consoleThread = new Thread(new ThreadStart(StartConsole));
        consoleThread.Start();

        Log.Logger = LoggerFactory.CreateDefaultLogger(ConfigurationLoader.CreateOptions(new LoggerConfig()));

        Log.Debug("Initializing application");

        Log.Debug("Initializing configuration loader");
        var _configLoader = new ConfigurationLoader(DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"), "Config/json");

        Log.Debug("Loading launch parameters");
        var launchConfig = _configLoader.LoadConfiguration<LaunchConfig>("launchConfig.json"); 

        Log.Debug("Launch parameters loaded");
        if(launchConfig.Environment == LaunchEnv.Development)
        {
            Log.Debug("Launching Stars Forsaken development environment");
            using var game = new StarsForsakenDev();
            game.Run();
        }
        else if(launchConfig.Environment == LaunchEnv.Production)
        {
            Log.Debug("Launching Stars Forsaken");
            using var game = new StarsForsaken();
            game.Run();
        }
        else if (launchConfig.Environment == LaunchEnv.Testing)
        {
            Log.Debug("Launching Stars Forsaken testing environment");
            using var game = new StarsForsakenTest();
            game.Run();
        }
    }

    static void StartConsole()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;
            else
                ConsoleInterpreter.ExecuteCommand(input);
            
            Console.Write("> ");
        }
    }

}