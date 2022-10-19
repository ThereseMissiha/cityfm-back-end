﻿using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Products.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        readonly RequestDelegate _next;
        readonly IWebHostEnvironment _env;
        readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            _next = next;
            _env = env;
            _logger = loggerFactory.CreateLogger(nameof(ErrorHandlerMiddleware));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteServerError(context.Response, ex);
            }
        }

        async Task WriteServerError(HttpResponse response, Exception ex)
        {
            _logger.LogError($"Global error:\n{ex.Message}\n{ex.InnerException?.Message}", ex);

            if (response.HasStarted)
                return;

            response.StatusCode = 500;
            if (_env.EnvironmentName != Environments.Production)
            {
                var error = JsonConvert.SerializeObject(ex, Formatting.Indented);
                await response.WriteAsync(error);
            }
        }
    }
}
