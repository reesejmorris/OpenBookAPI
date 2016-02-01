using Microsoft.AspNet.Builder;

namespace OpenBookAPI.HttpCache{
    public static class BuilderExtensions{
        public static IApplicationBuilder UseHttpCache(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpCacheMiddleware>();
        }
        
    }
}