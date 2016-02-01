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
            
            using (var memoryStream = new MemoryStream())
            {
                if(context.Request.Method != "GET")
                {
                    await _next.Invoke(context);
                    return;
                }   
                var url = context.Request.Path;
                _logger.Information($"GET request recieved ({url}), checking cache...");
                var resp = await _store.Get(url);
                if(resp ==null)
                {
                    _logger.Information("No cache entry found, invoke next");
                    await _next.Invoke(context);
                    //add response body to the store
                    if(context.Response.ContentType.StartsWith("application/json") && context.Response.StatusCode == 200)
                    {
                        await _store.Set(url,"{'HELLO':'HELLO'}" ,DateTime.Now.AddHours(1));
                        return;
                    }
                    else
                    {
                        _logger.Information($"Entry not cached, content type: ({context.Response.ContentType}), response code: ({context.Response.StatusCode})");
                    }     
                }        
                _logger.Information($"Found cache entry,{Environment.NewLine} {resp} {Environment.NewLine}  returning");
                //short circuit the pipeline and return the response from the cache
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength = resp.Length;
                var ms = new MemoryStream();
                await context.Response.Body.CopyToAsync(ms);
                using(var sw = new StreamWriter(ms))
                {
                    sw.Write(resp);
                }
                await ms.CopyToAsync(context.Response.Body);
            }
        } 
        protected void AddToCache(HttpContext context){
            
        }
    }
}