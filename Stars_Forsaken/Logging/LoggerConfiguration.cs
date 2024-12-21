using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stars_Forsaken.Config;

namespace Stars_Forsaken.Logging
{
    public class LoggerConfiguration : ConfigurationModel
    {
        public virtual string FileName { get; } = "stars_forsaken.log";

        public virtual string FolderName { get; } = "logs";
    }
}
