using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Stars_Forsaken.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        internal protected readonly LoggerConfiguration Config;
        internal protected readonly StreamWriter Writer;

        private string LogsPath { get; }


        // configures a path for a log file and creates a streamwriter
        //                   pass an appsettings.json here
        public LoggerProvider(IOptions<LoggerConfiguration> config, string baseDir)
        {
            Config = config.Value;
            var logDirectory = Path.Combine(baseDir, Config.FolderName);

            if(!Directory.Exists(logDirectory)) 
            {
                Directory.CreateDirectory(logDirectory);
            }

            LogsPath = Path.Combine(logDirectory, Config.FileName);
            if(!File.Exists(LogsPath))
            {
                File.Create(LogsPath);
            }

            Writer = new StreamWriter(new FileStream(LogsPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
        }

        public void Dispose()
        {
            Writer.Flush();
            Writer.Close();
        }

        public ILogger CreateLogger()
        {
            return new Logger(this);
        }
    }
}
