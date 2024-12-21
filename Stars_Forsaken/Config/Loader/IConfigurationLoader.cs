using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Stars_Forsaken.Config.Loader
{
    public interface IConfigurationLoader
    {
        /// <value>Property <c>ConfigurationFolder</c> represents the path to the configuration folder.</value>
        string ConfigurationFolder { get; }


        /// <summary>
        /// Changes the path of the configuration folder to <paramref name="configPath"/>.
        /// </summary>
        /// <param name="configPath">Absolute path to the configuration folder.</param>
        void ChangePath(string configPath);


        /// <summary>
        /// <para>
        /// Changes the path of the configuration folder to the directory titled <paramref name="configFolderName"/>
        /// within a <paramref name="path"/>.
        /// </para>
        /// <example>
        /// For example:
        /// <code>
        /// string path = "C:/Users/App";
        /// ChangePath(path, "Config");
        /// </code>
        /// </example>
        /// Results in the configuration folder being set to <c>"C:/Users/App/Config"</c>.
        /// </summary>
        /// <param name="path">An absolute path.</param>
        /// <param name="configFolderName">The name of the folder within the given path.</param>
        void ChangePath(string path, string configFolderName);


        /// <summary>
        /// Parses a JSON configuration file titled <paramref name="configFile"/> into a <typeparamref name="TConfig"/> object from the initialized root path.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configFile">Name of a configuration file.</param>
        /// <returns>A <typeparamref name="TConfig"/> object.</returns>
        TConfig LoadConfiguration<TConfig>(string configFile) where TConfig : ConfigurationModel, new();


        /// <summary>
        /// Parses a JSON configuration file titled <paramref name="configFile"/> into a dictionary of <typeparamref name="TConfig"/> objects from the initialized root path.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configFile">Name of a configuration file.</param>
        /// <returns><see cref="Dictionary{string, TConfig}"/>.</returns>
        Dictionary<string, TConfig> LoadConfigurationDictionary<TConfig>(string configFile) where TConfig : ConfigurationModel;
    }
}