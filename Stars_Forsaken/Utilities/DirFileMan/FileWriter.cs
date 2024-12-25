using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using Stars_Forsaken.Constants.Exceptions;

namespace Stars_Forsaken.Utilities.DirFileMan
{
    public class FileWriter : IDisposable
    {
        private static readonly Dictionary<string, FileWriter> ActiveWriters = new();
        private static readonly Dictionary<string, FileStreamOptions> StreamOptions = new()
        {
            {"read", new FileStreamOptions
            {
                Mode = FileMode.Open,
                Access = FileAccess.Read,
                Share = FileShare.Read,
                BufferSize = 4096,
                Options = FileOptions.SequentialScan
            }},

            {"write", new FileStreamOptions
            {
                Mode = FileMode.Open,
                Access = FileAccess.Write,
                Share = FileShare.Read,
                BufferSize = 4096,
                Options = FileOptions.None
            }},

            {"readwrite", new FileStreamOptions
            {
                Mode = FileMode.Open,
                Access = FileAccess.ReadWrite,
                Share = FileShare.ReadWrite,
                BufferSize = 4096,
                Options = FileOptions.SequentialScan
            }},

            {"append", new FileStreamOptions
            {
                Mode = FileMode.Append,
                Access = FileAccess.Write,
                Share = FileShare.ReadWrite,
                BufferSize = 4096,
                Options = FileOptions.SequentialScan
            }},
        };

        private FileStream _stream;
        private bool _disposed = false;

        public string FilePath { get; private set; }
        public string FileName { get; private set; }

        private FileWriter(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriter"/> class.
        /// </summary>
        /// <param name="filePath">Path to the file to be opened.</param>
        /// <param name="options">Configuration options to be used for the FileStream. Refer to the <see cref="Options(string))>"/> method for presets.</param>
        /// <returns>An instance of <see cref="FileWriter"/></returns>
        /// <exception cref="FileWriterException"></exception>
        public static FileWriter OpenFile(string filePath, FileStreamOptions options)
        {
            try
            {
                if (ActiveWriters.ContainsKey(filePath))
                    throw new InvalidOperationException($"File {filePath} is already opened.");

                var fileManager = new FileWriter(filePath)
                {
                    _stream = new FileStream(filePath, options)
                };

                ActiveWriters[fileManager.FileName] = fileManager;
                return fileManager;
            }
            catch (Exception e)
            {
                throw new FileWriterException($"Could not open file {filePath}.", e);
            }
        }


        /// <summary>
        /// Writes content to the file.
        /// </summary>
        /// <param name="content">Content to be written.</param>
        /// <exception cref="FileWriterException"></exception>
        public void Write(string content)
        {
            try
            {
                if (_stream == null)
                    throw new InvalidOperationException("File stream is not opened.");

                using var writer = new StreamWriter(_stream, leaveOpen: true);
                writer.Write(content);
            }
            catch (Exception e)
            {
                throw new FileWriterException($"Could not write to file {FilePath}.", e);
            }
        }

        /// <summary>
        /// Writes a line of content to the file.
        /// </summary>
        /// <param name="content">Content to be written.</param>
        /// <exception cref="FileWriterException"></exception>
        public void WriteLine(string content)
        {
            try
            {
                if (_stream == null)
                    throw new InvalidOperationException("File stream is not opened.");

                using var writer = new StreamWriter(_stream, leaveOpen: true);
                writer.WriteLine(content);
            }
            catch (Exception e)
            {
                throw new FileWriterException($"Could not write to file {FilePath}.", e);
            }
        }

        /// <summary>
        /// Creates custom configuration options for a <see cref="FileStream"/>.
        /// </summary>
        /// <param name="configureOptions">A collection of parameters for the <see cref="FileStreamOptions"/> class.</param>
        /// <returns>An instance of <see cref="FileStreamOptions"/></returns>
        /// <exception cref="FileWriterException"></exception>
        public static FileStreamOptions CustomOptions(params Action<FileStreamOptions>[] configureOptions)
        {
            try
            {
                var options = new FileStreamOptions();

                foreach (var configureOption in configureOptions)
                {
                    configureOption(options);
                }

                return options;
            }
            catch (Exception e)
            {
                throw new FileWriterException("Could not create custom file stream options.", e);
            }
        }

        /// <summary>
        /// Loads a preset configuration for a <see cref="FileStream"/>. Available preset keys:<br/>
        /// "read", "write", "readwrite", "append".
        /// </summary>
        /// <param name="option">Preset key</param>
        /// <returns>A <see cref="FileStreamOptions"/> from the collection of presets.</returns>
        public static FileStreamOptions Options(string option)
        {
            return StreamOptions[option];
        }

        /// <summary>
        /// Disposes of the <see cref="FileWriter"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Suppress finalization as resources are already released
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _stream.Dispose();
            }

            _disposed = true;
        }
    }
}
