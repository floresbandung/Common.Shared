using System;
using DD.Tata.Buku.Shared.Http;
using DD.Tata.Buku.Shared.Logs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DD.Tata.Buku.Common.Shared.Logs
{
    public class LogRequest : IRequest<ApiResult<LogResponse>>
    {
        public LogRequest()
        {
            Timestamp = DateTime.UtcNow;
        }
        public string Environment { get; set; }
        public string EventId { get; set; }
        public string State { get; set; }
        public DateTime Timestamp { get; private set; }
        public LogLevel Type { get; set; }
        public string Document { get; set; }
        public string ProviderDb { get; internal set; }
        public string CollectionName { get; set; }

        public string Message { get; set; }
        public string IpAddress { get; set; }
        public string MachineName { get; set; }
        public string OsVersion { get; set; }
    }
}