using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stars_Forsaken.Config.DirFilEdit
{
    /// <summary>
    /// Wrapper for the <see cref="File"/> class that exposes static methods for file manipulation.
    /// </summary>
    public class FilEdit
    {
        public static ILogger Logger { get; set; }

        public static FileStreamOptions ReadOptions = new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
            BufferSize = 4096,
            Options = FileOptions.SequentialScan
        };

        public static FileStreamOptions WriteOptions = new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Write,
            Share = FileShare.Read,
            BufferSize = 4096,
            Options = FileOptions.None
        };

        public static FileStreamOptions ReadWriteOptions = new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.ReadWrite,
            Share = FileShare.Read,
            BufferSize = 4096,
            Options = FileOptions.SequentialScan
        };

        public static FileStreamOptions AppendOptions = new FileStreamOptions
        {
            Mode = FileMode.Append,
            Access = FileAccess.Write,
            Share = FileShare.ReadWrite,
            BufferSize = 4096,
            Options = FileOptions.SequentialScan
        };

        public static bool ExistsFile(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not check if file exists at path {filePath}", e);
                return false;
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
                    Logger.LogInformation($"Created file at path {filePath}");
                    return filePath;
                }
                return filePath;
            }
            catch (Exception e)
            {
                Logger.LogError($"Cannot create file {filePath}", e);
                return null;
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
                Logger.LogInformation($"Deleted file at path {filePath}");
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not delete file at path {filePath}", e);
                return false;
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
                Logger.LogError($"Could not read file at path {filePath}", e);
                return null;
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
                Logger.LogError($"Could not read file at path {filePath}", e);
                return null;
            }
        }

        public static string[] ReadFileLines(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            return ReadFileLines(filePath);
        }

        public static FileStream OpenFileStream(string filePath, FileStreamOptions options)
        {
            try
            {
                var file = File.Open(filePath, options);
                Logger.LogInformation($"Opened file at path {filePath}");
                return file;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not open file at path {filePath}", e);
                return null;
            }
        }

        public static FileStream OpenFileStream(string path, string fileName, FileStreamOptions options)
        {
            string filePath = Path.Combine(path, fileName);

            return OpenFileStream(filePath, options);
        }

        public static void CloseFileStream(FileStream fileStream)
        {
            try
            {
                Logger.LogInformation($"File {fileStream.Name} closed");
                fileStream.Close();
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not close file {fileStream.Name}", e);
            }
        }
    }
}
