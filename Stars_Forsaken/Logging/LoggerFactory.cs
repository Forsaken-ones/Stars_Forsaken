using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Stars_Forsaken.Config.Models;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Stars_Forsaken.Utilities.DirFileMan;

namespace Stars_Forsaken.Logging
{
    public static class LoggerFactory
    {
        private static string _baseDirectory = DirManager.GetParentDir(AppContext.BaseDirectory, "Stars_Forsaken");
        public static Logger CreateLogger(params Action<LoggerConfiguration>[] configureActions)
        {
            var loggerConfig = new LoggerConfiguration();

            foreach (var configureAction in configureActions)
            {
                configureAction(loggerConfig);
            }

            return loggerConfig.CreateLogger();
        }

        public static Logger CreateDefaultLogger(IOptions<LoggerConfig> options)
        {
            return new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(Path.Combine(_baseDirectory, Path.Combine(options.Value.FolderName, options.Value.FileName)),
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
