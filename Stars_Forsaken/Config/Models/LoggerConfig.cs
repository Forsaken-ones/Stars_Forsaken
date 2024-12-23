using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stars_Forsaken.Config.Models
{
    public class LoggerConfig : ConfigurationModel
    {
        public virtual string FileName { get; }

        public virtual string FolderName { get; }

        public LoggerConfig() : base()
        {
            FileName = "stars_forsaken.log";
            FolderName = "logs";
        }
    }
}
