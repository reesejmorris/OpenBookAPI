using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using Newtonsoft.Json;
using Serilog;

namespace OpenBookAPI.HttpCache
{
    public class HttpCacheActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private ActionExecutionDelegate _next;
        private readonly ICacheStore _store;

        public HttpCacheActionFilter(ICacheStore store, ILogger logger)
        {
            _store = store;
            _logger = logger;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                await HandleQueryRequest(context, next);
            }
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                await HandleQueryResponse(context);
            }
            else
            {
                await HandleCommandResponse(context);
            }
            await next();
        }

        private async Task HandleQueryResponse(ResultExecutingContext context)
        {
            if (context.Result.GetType() != typeof (Microsoft.AspNet.Mvc.ObjectResult))
                return;

            var url = context.Controller.GetType() + "|" + context.ActionDescriptor.DisplayName;
            _logger.Debug("No cache entry found, invoke next");
            //add response body to the store
            if (context.HttpContext.Response.StatusCode == 200)
            {
                var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(((Microsoft.AspNet.Mvc.ObjectResult)context.Result).Value);
                await _store.Set(url, jsonResponse, DateTime.Now.AddHours(1));
            }
            else
            {
                _logger.Debug($"Entry not cached, content type: ({context.HttpContext.Response.ContentType}), response code: ({context.HttpContext.Response.StatusCode})");
            }
        }

        private async Task HandleCommandResponse(ResultExecutingContext context)
        {
            var url = context.Controller.GetType() +"|"+ context.ActionDescriptor.DisplayName;
            if (context.HttpContext.Response.StatusCode == 200)
            {
                _logger.Information($"Invalidating ({url})");
                await _store.Invalidate(url);
            }
        }
        private async Task HandleQueryRequest(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var url = context.Controller.GetType() + "|" + context.ActionDescriptor.DisplayName;
            _logger.Information($"GET request recieved ({url}), checking cache...");
            
            var resp = await _store.Get(url);
            if (resp == null)
                await next();
            else
            {
                context.Result = new JsonResult(JsonConvert.DeserializeObject(resp));
                _logger.Information($"Found cache entry {url} returning");
            }
        }
    }
}
