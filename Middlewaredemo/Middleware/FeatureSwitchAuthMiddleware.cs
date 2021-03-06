using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middlewaredemo.Middleware
{
    public class FeatureSwitchAuthMiddleware
    {
        //importing the _next Dependency Injection
        private readonly RequestDelegate _next;

        public FeatureSwitchAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration config)
        {
            var endpoint = httpContext.GetEndpoint()?.Metadata.GetMetadata<RouteAttribute>();
            if (httpContext.Request.Path.Value.Contains("/feature"))
            {
                var switches = config.GetSection("FeatureSwitches");
                var report = switches.GetChildren().Select(x => $"{x.Key} : {x.Value}");
                //shortcircuting the pipeline in order to generate response
                await httpContext.Response.WriteAsync(string.Join("\n", report));
            }
            else
            {
                //if the url does not contain features allow
                //the middleware to continue 
                await _next(httpContext);
            }
        }
    }
}
