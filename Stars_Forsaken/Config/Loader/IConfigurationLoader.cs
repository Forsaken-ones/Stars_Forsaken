using System.Collections.Generic;

namespace Stars_Forsaken.Config.Loader
{
    public interface IConfigurationLoader
    {
        string ConfigurationFolder { get; }


        /// <summary>
        /// Changes the path of the configuration folder.
        /// </summary>
        /// <param name="configPath"></param>
        void ChangePath(string configPath);


        /// <summary>
        /// Changes the path of the configuration folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="configFolderName"></param>
        void ChangePath(string path, string configFolderName);


        /// <summary>
        /// Parses a JSON configuration file into a <typeparamref name="TConfig"/> object from the initialized root path.
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="configFile"></param>
        /// <returns>A <typeparamref name="TConfig"/> object or <see langword="null"/>.</returns>
        TConfig LoadConfiguration<TConfig>(string configFile) where TConfig : ConfigurationModel, new();


        /// <summary>
        /// Parses a JSON configuration file into a dictionary of <typeparamref name="TConfig"/> objects from the initialized root path.
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="configFile"></param>
        /// <returns>A <see cref="Dictionary{string, TConfig}"/> or <see langword="null"/>.</returns>
        Dictionary<string, TConfig> LoadConfigurationDictionary<TConfig>(string configFile) where TConfig : ConfigurationModel;
    }
}