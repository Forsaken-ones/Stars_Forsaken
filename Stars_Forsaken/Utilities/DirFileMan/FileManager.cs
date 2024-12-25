using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Stars_Forsaken.Constants.Exceptions;

namespace Stars_Forsaken.Utilities.DirFileMan
{
    /// <summary>
    /// Wrapper for the <see cref="File"/> class that exposes static methods for file manipulation.
    /// </summary>
    public static class FileManager
    {
        public static bool ExistsFile(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception e)
            {
                throw new FileManagementException("Could not check if file exists at " + filePath, e);
            }
        }

        public static bool ExistsFile(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return ExistsFile(filePath);
        }

        public static string CreateFile(string filePath)
        {
            try
            {
                if (!ExistsFile(filePath))
                {
                    File.Create(filePath);
                    Log.Debug("Created file at {Path}", filePath);
                    return filePath;
                }
                return filePath;
            }
            catch (Exception e)
            {
                throw new FileManagementException("Could not create file at " + filePath, e);
            }
        }

        public static string CreateFile(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return CreateFile(filePath);
        }

        /// <summary>
        /// Deletes a file at a given path.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> otherwise</returns>
        public static bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                Log.Debug("Deleted file at {Path}", filePath);
                return true;
            }
            catch (Exception e)
            {
                throw new FileManagementException("Could not delete file at " + filePath, e);
            }
        }

        /// <summary>
        /// Deletes a file of a given name at a given path.
        /// </summary>
        /// <param name="fileName">Name of the file to be deleted.</param>
        /// <param name="path">Path to the file's directory.</param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> otherwise</returns>
        public static bool DeleteFile(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return DeleteFile(filePath);
        }

        public static string ReadFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                throw new FileManagementException("Could not read file at " + filePath, e);
            }
        }

        public static string ReadFile(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return ReadFile(filePath);
        }

        public static string[] ReadFileLines(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                throw new FileManagementException("Could not read file at " + filePath, e);
            }
        }

        public static string[] ReadFileLines(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return ReadFileLines(filePath);
        }
    }
}
