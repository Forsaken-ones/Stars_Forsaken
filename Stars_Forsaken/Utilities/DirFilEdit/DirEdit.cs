using Serilog;
using System;
using System.IO;

namespace Stars_Forsaken.Utilities.DirFilEdit
{
    /// <summary>
    /// Wrapper for the <see cref="Directory"/> class that exposes static methods for directory manipulation.
    /// </summary>
    public class DirEdit
    {
        public static string GetParentDir(string path)
        {
            try
            {
                return Directory.GetParent(path).FullName;
            }
            catch (Exception e)
            {
                Log.Error(e, "Cannot access parent directory of {Path}", path);
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
                    Log.Debug("Created directories and/or subdirectories at {Path}", dirPath);
                }

                return dirPath;
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not create directories and/or subdirectories at {Path}", dirPath);
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
                Log.Debug("Deleted directories and/or subdirectories at {Path}", dirPath);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not delete directories and/or subdirectories at {Path}", dirPath);
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
                Log.Error(e, "Could not check if directories and/or subdirectories exist at {Path}", dirPath);
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
