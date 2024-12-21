using Microsoft.Extensions.Logging;

namespace Stars_Forsaken.Logging
{
    public interface ILoggerProvider
    {
        ILogger CreateLogger();
        void Dispose();
    }
}