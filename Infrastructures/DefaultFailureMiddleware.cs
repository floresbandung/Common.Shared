using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DD.Tata.Buku.Shared.Fault;
using DD.Tata.Buku.Shared.Logs;
using DD.TataBuku.Shared.Fault;

namespace DD.Tata.Buku.Shared.Infrastructures
{
    public class DefaultFailureMiddleware
    {
        private readonly RequestDelegate _next;

        public DefaultFailureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLogger logger)
        {
            var currentBody = context.Response.Body;


            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            ErrorResponse error = null;
            try
            {
                await _next(context);
            }
            catch (ApiException apiException)
            {
                logger.LogException(apiException, apiException.EventId);
                context.Response.StatusCode = (int)apiException.StatusCode;
                error = new ErrorResponse
                {
                    ErrorCode = apiException.ErrorCode,
                    ErrorDescription = apiException.Message
                };
            }
            catch (Exception e)
            {
                logger.LogException(e);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error = new ErrorResponse
                {
                    ErrorCode = (int)HttpStatusCode.InternalServerError,
                    ErrorDescription = StaticMessage.DEFAULT_ERROR_MESSAGE
                };
            }

            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            var readToEnd = await new StreamReader(memoryStream).ReadToEndAsync();

            if (string.Compare(context.Response.ContentType, "application/pdf", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(context.Response.ContentType, "image/png", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                var buffer = memoryStream.ToArray();
                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return;
            }

            if (context.Response.StatusCode == 200 && context.Response.ContentType != "application/json")
            {
                await context.Response.WriteAsync(readToEnd);
                return;
            }

            var objResult = JsonConvert.DeserializeObject(readToEnd);
            if (context.Response.StatusCode != 200 && error == null)
            {
                error = new ErrorResponse
                {
                    ErrorCode = (int)HttpStatusCode.InternalServerError,
                    ErrorDescription = StaticMessage.DEFAULT_ERROR_MESSAGE
                };
                context.Response.Headers.Remove("Content-Length");
            }

            if (error != null)
            {
                
                context.Response.ContentType = "application / json; charset = utf - 8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));

            }
            else
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(objResult));
            }
        }
    }
}
