using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace dotnetcoresite
{
    internal class  LoggingMiddleware
    {
        private readonly ILogger _logger;
        private RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, ILogger<Startup> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("got it");
            var startTime = DateTime.Now;
            _logger.LogInformation($"Starting request timer at " +
            $"{startTime} for request " +
            $"{context.Request.GetDisplayUrl()} on thread id " +
            $"{Thread.CurrentThread.ManagedThreadId}");

            await _next(context);

            var stopTime = DateTime.Now;
            _logger.LogInformation($"Stopping request timer at " +
            $"{stopTime} for request " +
            $"{context.Request.GetDisplayUrl()} on thread id " +
            $"{Thread.CurrentThread.ManagedThreadId}");

        }
    }
}