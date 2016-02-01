using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Serilog;

namespace OpenBookAPI.HttpCache{
    public class HttpCacheMiddleware{
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly ICacheStore _store;
        public HttpCacheMiddleware(RequestDelegate next, ICacheStore store, ILogger logger){
            _next = next;
            _store = store;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context){
            if(context.Request.Method == "GET")
            {
                await HandleQuery(context);
            }   
            else 
            {
                await HandleCommand(context);
            }
            
        } 
        protected async Task HandleCommand(HttpContext context){
            var url = context.Request.Path;
            await _next.Invoke(context);
            if(context.Response.StatusCode == 200)
            {
                _logger.Information($"Invalidating ({url})");
                await _store.Invalidate(url);
            }
            
        }
        protected async Task HandleQuery(HttpContext context){
            using (var memoryStream = new MemoryStream())
            {
                var bodyStream = context.Response.Body;
                context.Response.Body = memoryStream;
                
                var url = context.Request.Path;
                _logger.Information($"GET request recieved ({url}), checking cache...");
                var resp = await _store.Get(url);
                if(resp == null)
                {
                    _logger.Information("No cache entry found, invoke next");
                    await _next.Invoke(context);
                    //add response body to the store
                    if(context.Response.ContentType.StartsWith("application/json") && context.Response.StatusCode == 200)
                    {
                        var jsonResponse = string.Empty;
                        using(var sr = new StreamReader(memoryStream)){
                            memoryStream.Seek(0,SeekOrigin.Begin);
                            jsonResponse = await sr.ReadToEndAsync();
                            memoryStream.Seek(0,SeekOrigin.Begin);
                            await _store.Set(url,jsonResponse ,DateTime.Now.AddHours(1));
                            await memoryStream.CopyToAsync(bodyStream);
                        }
                        return;
                    }
                    else
                    {
                        _logger.Information($"Entry not cached, content type: ({context.Response.ContentType}), response code: ({context.Response.StatusCode})");
                    }     
                }        
                _logger.Information($"Found cache entry {url} returning");
                //await context.Response.WriteAsync(resp);
                await context.Response.WriteAsync(resp);
                context.Response.ContentType = "application/json";
                memoryStream.Seek(0,SeekOrigin.Begin);
                await memoryStream.CopyToAsync(bodyStream);
            }
        }
    }
}