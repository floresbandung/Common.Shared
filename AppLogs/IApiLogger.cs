using Microsoft.Extensions.Logging;
using System;

namespace DD.Tata.Buku.Shared.Logs
{
    public interface IApiLogger
    {
        void Log<T>(LogLevel level, EventId eventId, T state, Exception exception = null, Func<T, Exception, string> formatter = null);
        void Log<T>(string message, LogLevel level, EventId eventId, T state);
        void LogInformation<T>(T contents, string eventId = "0");
        void LogException(Exception ex, string eventId = "0");
        void LogException(string message, Exception ex, string eventId);
        void Debug<T>(string message, T content, string eventId = "0");
        void Debug<T>(string message, string eventId = "0");
        void Debug<T>(T content, string eventId = "0");
        void LogInformation(string message, int eventId = 0);
    }
}