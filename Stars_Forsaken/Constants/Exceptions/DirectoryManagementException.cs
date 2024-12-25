using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stars_Forsaken.Constants.Exceptions
{
    public class DirectoryManagementException : Exception
    {
        public DirectoryManagementException() : base() { }
        public DirectoryManagementException(string message) : base(message) { }
        public DirectoryManagementException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
