using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace dotnetcoresite
{
    internal class  ReqMsgMiddleware
    {
        private readonly ILogger _logger;
        private RequestDelegate _next;

        public ReqMsgMiddleware(RequestDelegate next, ILogger<Startup> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("Read Request Body");

            try
            {
                using (StreamReader sr = new StreamReader(context.Request.Body)) 
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) 
                    {
                        body.AppendLine(line);
                    }
                }
            }
            catch (Exception e) 
            {
                _logger.LogInformation(e.Message);
            }

            _logger.LogInformation(body.ToString());
            await _next(context);
        }
    }
}