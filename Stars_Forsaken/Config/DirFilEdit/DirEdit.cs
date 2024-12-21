using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Stars_Forsaken.Config.DirFilEdit
{
    /// <summary>
    /// Wrapper for the <see cref="Directory"/> class that exposes static methods for directory manipulation.
    /// </summary>
    public class DirEdit
    {
        public static ILogger Logger { get; set; }

        public static string GetParentDir(string path)
        {
            try
            {
                return Directory.GetParent(path).FullName;
            }
            catch (Exception e)
            {
                Logger.LogError($"Cannot access parent directory of {path}", e);
                return null;
            }
        }

        public static string GetParentDir(string path, int levels)
        {
            string parentDir = path;
            for (int i = 0; i < levels; i++)
            {
                parentDir = GetParentDir(parentDir);
            }
            return parentDir;
        }

        public static string GetParentDir(string path, string dirName)
        {
            string parentDir = path;
            while (Directory.GetParent(parentDir).Name != dirName)
            {
                parentDir = GetParentDir(parentDir);
            }
            parentDir = GetParentDir(parentDir);
            return parentDir;
        }

        public static string GetParentDir(string path, string dirName, int levels)
        {
            string parentDir = path;
            while (Directory.GetParent(parentDir).Name != dirName)
            {
                parentDir = GetParentDir(parentDir);
            }
            for (int i = 0; i < levels; i++)
            {
                parentDir = GetParentDir(parentDir);
            }
            return parentDir;
        }

        public static string CreateDir(string dirPath)
        {
            try
            {
                if(!ExistsDir(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    Logger.LogInformation($"Created directories and/or subdirectories at path {dirPath}");
                }

                return dirPath;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not create directories and/or subdirectories at path {dirPath}", e);
                return null;
            }
        }

        public static string CreateDir(string path, string dirName)
        {
            string dirPath = Path.Combine(path, dirName);

            return CreateDir(dirPath);
        }

        public static bool DeleteDir(string dirPath)
        {
            try
            {
                Directory.Delete(dirPath, true);
                Logger.LogInformation($"Deleted directories and/or subdirectories at path {dirPath}");
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not delete directories and/or subdirectories at path {dirPath}", e);
                return false;
            }
        }

        public static bool DeleteDir(string path, string dirName)
        {
            string dirPath = Path.Combine(path, dirName);

            return DeleteDir(dirPath);
        }

        public static bool ExistsDir(string dirPath)
        {
            try
            {
                return Directory.Exists(dirPath);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not check if directories and/or subdirectories exist at path {dirPath}", e);
                return false;
            }
        }

        public static bool ExistsDir(string path, string dirName)
        {
            string newDir = Path.Combine(path, dirName);
            
            return ExistsDir(newDir);
        }
    }
}
