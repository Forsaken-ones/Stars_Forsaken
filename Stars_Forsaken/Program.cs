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

namespace Stars_Forsaken;
public class Program
{
    static void Main(string[] args)
    {
        Log.Logger = LoggerFactory.CreateDefaultLogger(ConfigurationLoader.CreateOptions(new LoggerConfig()));

        Log.Debug("Initializing application");

        Log.Debug("Initializing configuration loader");
        var _configLoader = new ConfigurationLoader(DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"), "Config/json");

        Log.Debug("Loading launch parameters");
        var launchConfig = _configLoader.LoadConfiguration<LaunchConfig>("launchConfig.json");

        Log.Debug("Launch parameters loaded");
    }
    
}
/*
void InitializeApplication()    
{
    Stars_Forsaken.Logging.ILoggerProvider _loggerProvider;
    ILogger _logger;
    IConfigurationLoader _configLoader;

    var loggerConfig = new LoggerConfiguration();
    _loggerProvider = new LoggerProvider(ConfigurationLoader.CreateOptions(loggerConfig), DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"));

    _logger = _loggerProvider.CreateLogger();
    _logger.LogCritical("Initializing application");
    try
    {
        throw new Exception("Balls");
    }
    catch (Exception e)
    {
        _logger.LogError(e, "Test");
    }

    DirEdit.Logger = _logger;
    FilEdit.Logger = _logger;

    _logger.LogInformation($"Initializing configuration loader");
    _configLoader = new ConfigurationLoader(DirEdit.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken"), "Config/json", _logger);

    _logger.LogInformation($"Loading program launch configuration");
    var launchConfig = _configLoader.LoadConfiguration<LaunchConfig>("launchConfig.json");

    _logger.LogInformation($"Selected environment");
}
*/