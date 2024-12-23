using Stars_Forsaken.Config.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stars_Forsaken.Config.Models
{
    internal class LaunchConfig : ConfigurationModel
    {
        public LaunchEnv Environment { get; }
        public LaunchConfig() : base()
        { 
            Environment = LaunchEnv.Development;
        }
    }
}
