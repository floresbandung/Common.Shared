using System;
using DD.Tata.Buku.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DD.Tata.Buku.Shared.Logs
{
    public class ApiLogger : IApiLogger
    {
        private readonly ILogger<ApiLogger> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerSettings _settings;

        public ApiLogger(ILogger<ApiLogger> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 2,
                Error = OnSerializingError
            };
        }

        private void OnSerializingError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.CurrentObject);
            Console.WriteLine(e.ErrorContext);
        }


        public void LogException(Exception ex, string eventId)
        {
            _logger.Log(LogLevel.Critical, eventId, ex, ex.Message);
        }
        
        public void LogException(string message, Exception ex, string eventId = "0")
        {
            _logger.Log(LogLevel.Critical, eventId, ex, message, _httpContextAccessor.HttpContext.TraceIdentifier);
        }

        public void Debug<T>(string message, T content, string eventId = "0")
        {
            _logger.Log(LogLevel.Debug, new EventId(eventId.ToInteger()), content, null, (h, e) => message);
        }

        public void Debug<T>(string message, string eventId = "0")
        {
            _logger.Log<T>(LogLevel.Debug, new EventId(eventId.ToInteger()), default, null, (h, e) => message);
        }

        public void Debug<T>(T content, string eventId = "0")
        {
            _logger.Log(LogLevel.Debug, new EventId(eventId.ToInteger()), content, null, (h, e) => string.Empty);
        }

        public void LogInformation<T>(T contents, string eventId = "0")
        {
            var json = JsonConvert.SerializeObject(contents,_settings);
            
            /*if (json.StartsWith("{") && json.EndsWith("}") ||
                json.StartsWith("[") && json.EndsWith("]")) 
            {
                if (_hasMongoConnection)
                {
#pragma warning disable 4014
                    _logServices.Log(_databaseName, _collectionName, new LogRequest
#pragma warning restore 4014
                    {
                        Document = $"{json}",
                        Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                        State = "",
                        Type = LogLevel.Information,
                        CollectionName = _collectionName,
                        EventId = eventId,
                        ProviderDb = _databaseName,
                        IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        MachineName = Environment.MachineName,
                        OSVersion = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"]
                    });
                }
                _logger.LogInformation(eventId, json);
            }
            else
            {
                if (_hasMongoConnection)
                {
#pragma warning disable 4014
                    _logServices.Log(_databaseName, _collectionName, new LogRequest
#pragma warning restore 4014
                    {
                        Document = $"{JsonConvert.SerializeObject(new {Message = contents.ToString()})}",
                        Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                        State = "",
                        Type = LogLevel.Information,
                        CollectionName = _collectionName,
                        EventId = eventId,
                        ProviderDb = _databaseName,
                        Message = contents.ToString(),
                        IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        MachineName = Environment.MachineName,
                        OSVersion = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"]
                    });
                }
                _logger.LogInformation(eventId, contents.ToString());
            }*/
        }

        public void Log<T>(LogLevel level, EventId eventId, T state, Exception exception = null, Func<T, Exception, string> formatter = null)
        {
            _logger.Log(level, eventId, state, exception, formatter);
        }

        public void Log<T>(string message, LogLevel level, EventId eventId, T state)
        {
            _logger.Log(level, eventId, state, null, (h, e) => message );
        }

        public static string NewId()
        {
            return  Environment.MachineName + "_" + (long)(DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1))).TotalMilliseconds;

        }
    }
}