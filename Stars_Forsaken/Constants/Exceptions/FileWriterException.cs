using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stars_Forsaken.Constants.Exceptions
{
    public class FileWriterException : Exception
    {
        public FileWriterException() : base() { }
        public FileWriterException(string message) : base(message) { }
        public FileWriterException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
