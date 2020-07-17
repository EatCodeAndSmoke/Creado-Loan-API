using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CredoLoan.Api.Middlewares {
    public class RequestResponseLoggingMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context) {
            string reqStr = await FormatBodyAsync(context.Request);
            await _next(context);
            string respStr = await ReadResponseAsync(context.Response);
            var finalLog = new {
                context.TraceIdentifier,
                reqStr,
                respStr
            };
            _logger.LogInformation(JsonConvert.SerializeObject(finalLog));
        }

        private async Task<string> FormatBodyAsync(HttpRequest request) {
            var requestLogModel = new {
                request.Scheme,
                Url = $"{request.Host.Value}{request.Path.Value}{request.QueryString.Value}",
                request.Method,
                Body = await ReadRequestBodyAsync(request)
            };

            return JsonConvert.SerializeObject(requestLogModel);
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request) {
            string bodyStr = string.Empty;
            request.EnableBuffering();
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true)) {
                bodyStr = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }
            return bodyStr;
        }

        private async Task<string> ReadResponseAsync(HttpResponse response) {
            var originalBodyStream = response.Body;

            try {
                var memoryBodyStream = new MemoryStream();
                response.Body = memoryBodyStream;
                memoryBodyStream.Seek(0, SeekOrigin.Begin);
                string body = await new StreamReader(memoryBodyStream).ReadToEndAsync();
                memoryBodyStream.Seek(0, SeekOrigin.Begin);
                await memoryBodyStream.CopyToAsync(originalBodyStream);
                return body;
            } finally {
                response.Body = originalBodyStream;
            }
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
