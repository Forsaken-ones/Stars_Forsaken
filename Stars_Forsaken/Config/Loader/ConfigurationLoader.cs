#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Stars_Forsaken.Config.DirFilEdit;

namespace Stars_Forsaken.Config.Loader
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly string _defaultPath = AppContext.BaseDirectory;
        private readonly ILogger _logger;
        public string ConfigurationFolder { get; private set; }


        public ConfigurationLoader(string parentDir, string configFolder, ILogger logger)
        {
            _logger = logger;

            var maybeConfigFolder = Path.Combine(parentDir, configFolder);
            if(!DirEdit.ExistsDir(maybeConfigFolder))
            {
                _logger.LogWarning($"Configuration folder \"{maybeConfigFolder}\" not found. " +
                    $"Attempting to create folder");

                DirEdit.CreateDir(maybeConfigFolder);
                if(!DirEdit.ExistsDir(maybeConfigFolder))
                {
                    _logger.LogWarning($"Could not create configuration folder. Setting to default: \"{_defaultPath}\"");

                    ConfigurationFolder = _defaultPath;
                }
                else
                {
                    ConfigurationFolder = maybeConfigFolder;
                }
            }
            else
            {
                ConfigurationFolder = maybeConfigFolder;
            }

            _logger.LogInformation($"Configuration loader initialized at path {ConfigurationFolder}");
        }
        // pass a base directory and a realtive path to it, e.g. "Config/json"
        // initializes the loader to take config jsons from the given directory
        // if config folder is not found and cannot be created, defaults to the base directory


        public void ChangePath(string configFolderPath)
        {
            if(!DirEdit.ExistsDir(configFolderPath))
            {
                _logger.LogWarning($"Configuration folder \"{configFolderPath}\" not found, no changes made");
            }
            else
            {
                ConfigurationFolder = configFolderPath;
                _logger.LogInformation($"Configuration folder changed to \"{configFolderPath}\"");
            }
        }

        public void ChangePath(string path, string configFolderName)
        {
            var configFolderPath = Path.Combine(path, configFolderName);

            ChangePath(configFolderPath);
        }


        public TConfig? LoadConfiguration<TConfig>(string configFileName) where TConfig : ConfigurationModel, new()
        {
            try
            {
                var configFile = Path.Combine(ConfigurationFolder, configFileName);
                if (!FilEdit.ExistsFile(configFile))
                {
                    _logger.LogWarning($"Configuration file \"{configFileName}\" not found. " +
                        $"Attempting to create file from default template");

                    FilEdit.CreateFile(configFile);
                    if(!FilEdit.ExistsFile(configFile))
                    {
                        _logger.LogWarning($"Could not create configuration file, loading default values");
                        return new TConfig();
                    }
                }

                var jsonData = FilEdit.ReadFile(configFile);

                return JsonSerializer.Deserialize<TConfig>(jsonData);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not load configuration, loading default values", e);
                return new TConfig();
            }
        }
        // pass a json file name, e.g. "loggerconfig.json"
        // returns a deserialized object of the given type from the json file


        public Dictionary<string, TConfig>? LoadConfigurationDictionary<TConfig>(string configFileName) where TConfig : ConfigurationModel
        {
            try
            {
                var configFile = Path.Combine(ConfigurationFolder, configFileName);
                if (!FilEdit.ExistsFile(configFile))
                {
                    _logger.LogWarning($"Configuration file \"{configFileName}\" not found. " +
                        $"Attempting to create file from default template");

                    FilEdit.CreateFile(configFile);
                    if (!FilEdit.ExistsFile(configFile))
                    {
                        _logger.LogWarning($"Could not create configuration file, loading default values");
                        return new Dictionary<string, TConfig>();
                    }
                }

                var jsonData = FilEdit.ReadFile(configFile);

                return JsonSerializer.Deserialize<Dictionary<string, TConfig>>(jsonData);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not load configuration, loading default values", e);
                return new Dictionary<string, TConfig>();
            }
        }
        // similar to LoadConfiguration, but returns a dictionary of deserialized objects
        // MAY BE FLAWED IMPLEMENTATION IN CASE OF ERRORS, AVOID USING




        /// <summary>
        /// Crates an <see cref="IOptions{TConfig}"/> object from a <typeparamref name="TConfig"/> object.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="config">A <typeparamref name="TConfig"/> configuration object.</param>
        /// <returns>An <see cref="IOptions{TConfig}"/> object.</returns>
        public static IOptions<TConfig> CreateOptions<TConfig>(TConfig config) where TConfig : notnull, ConfigurationModel
        {
            return Options.Create(config);
        }
        // creates an IOptions object from a Configuration object
        // IOptions is used to pass configuration objects to services
        // e.g. can be used to pass keyboard controlls configuration to a MovementController or an InputManager
    }
}
