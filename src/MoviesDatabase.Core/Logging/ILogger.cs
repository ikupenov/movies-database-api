using System;

namespace MoviesDatabase.Core.Logging
{
    public interface ILogger
    {
        void Log(string text);

        void LogError(Exception ex);
    }
}
