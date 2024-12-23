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
using Stars_Forsaken.Utilities.DirFilEdit;
using Stars_Forsaken.Config.Models;
using System.CodeDom;
using Serilog;

namespace Stars_Forsaken.Utilities.ConfigLoader
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly string _defaultPath = AppContext.BaseDirectory;
        public string ConfigurationFolder { get; private set; }


        public ConfigurationLoader(string parentDir, string configFolder)
        {
            var maybeConfigFolder = Path.Combine(parentDir, configFolder);
            if (!DirEdit.ExistsDir(maybeConfigFolder))
            {
                Log.Warning("Configuration folder {Folder} not found. Attempting to create folder", maybeConfigFolder);

                DirEdit.CreateDir(maybeConfigFolder);
                if (!DirEdit.ExistsDir(maybeConfigFolder))
                {
                    Log.Warning("Could not create configuration folder. Setting to default: {Path}", _defaultPath);

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

            Log.Debug("Configuration loader initialized at {Path}", ConfigurationFolder);
        }
        // pass a base directory and a realtive path to it, e.g. "Config/json"
        // initializes the loader to take config jsons from the given directory
        // if config folder is not found and cannot be created, defaults to the base directory


        public void ChangePath(string configFolderPath)
        {
            if (!DirEdit.ExistsDir(configFolderPath))
            {
                Log.Warning("Configuration folder {Folder} not found, no changes made", configFolderPath);
            }
            else
            {
                ConfigurationFolder = configFolderPath;
                Log.Debug("Configuration folder changed to {Folder}", configFolderPath);
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
                if (!FilEdit.ExistsFile(configFile) || FilEdit.ReadFile(configFile) == "")
                {
                    Log.Warning("Configuration file {File} not found. Attempting to create file from default template", configFileName);

                    FilEdit.CreateFile(configFile);
                    if (!FilEdit.ExistsFile(configFile))
                    {
                        Log.Warning("Could not create configuration file, loading default values");
                        return new TConfig();
                    }
                    else
                    {
                        Log.Debug("Configuration file {File} created from default template", configFileName);
                        var serialized = JsonSerializer.Serialize(new TConfig());
                        using (var fs = FilEdit.OpenFileStream(configFile, FilEdit.ReadWriteOptions))
                        {
                            using (var writer = new StreamWriter(fs))
                            {
                                writer.Write(serialized);
                                Log.Debug("Default configuration written to {File}", configFileName);
                            }
                        }
                    }
                }

                var jsonData = FilEdit.ReadFile(configFile);
                Log.Debug("Configuration file opened and read");

                return JsonSerializer.Deserialize<TConfig>(jsonData);
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not load configuration, loading default values");
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
                    Log.Warning("Configuration file {File} not found. Attempting to create file from default template", configFileName);

                    FilEdit.CreateFile(configFile);
                    if (!FilEdit.ExistsFile(configFile))
                    {
                        Log.Warning("Could not create configuration file, loading default values");
                        return new Dictionary<string, TConfig>();
                    }
                }

                var jsonData = FilEdit.ReadFile(configFile);

                return JsonSerializer.Deserialize<Dictionary<string, TConfig>>(jsonData);
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not load configuration, loading default values");
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


