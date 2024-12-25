using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stars_Forsaken.Constants.Exceptions
{
    public class FileManagementException : Exception
    {
        public FileManagementException() : base() { }
        public FileManagementException(string message) : base(message) { }
        public FileManagementException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
