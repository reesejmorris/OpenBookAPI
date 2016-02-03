using System.IO;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Internal;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Swashbuckle.Swagger;
using OpenBookAPI.HttpCache;

namespace OpenBookAPI
{
    public class Startup
    {
        private RouteBuilder _routeBuilder;
        private IRouter _router;
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _router = new RouteCollection();
            var builder = new ConfigurationBuilder()
                                .SetBasePath(appEnv.ApplicationBasePath)
                                .AddJsonFile("config.json")
                                .AddEnvironmentVariables();
                                
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.WriteTo.RollingFile(Path.Combine(appEnv.ApplicationBasePath,"log-{Date}.txt"))
                .WriteTo.Trace()
                .WriteTo.Console()
                .CreateLogger();
            Configuration = builder.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var cacheStore = new CacheStore();
            services.AddMvc(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter(Log.Logger));
                options.Filters.Add(new HttpCacheActionFilter(cacheStore, Log.Logger));
            });

            //CORS -- temporary currently allow anyone to connect
            var OpenBookAPIcors = new CorsPolicy();
            OpenBookAPIcors.Headers.Add("*");
            OpenBookAPIcors.Origins.Add("*");
            OpenBookAPIcors.Methods.Add("*");
            OpenBookAPIcors.SupportsCredentials = true;
            services.AddCors(cors => cors.AddPolicy("OpenBookAPI", OpenBookAPIcors));


            //Dependancy Injection
            Modules.Register(services);
            services.AddInstance(typeof(IConfiguration),Configuration);
            services.AddInstance(typeof(ICacheStore), cacheStore);
            services.AddInstance<Serilog.ILogger>(Log.Logger);
            services.AddInstance<IRouter>(_router);
           

            

            //Swagger
            services.AddSwagger();
            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "OpenBook API",
                    Description = "The API Backend for the OpenBookApp",
                    TermsOfService = "No Potatos.",
                });

            });
            

            //services.ConfigureSwaggerSchema(options =>
            //{
            //    options.DescribeAllEnumsAsStrings = true;
            //});
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app,ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseIISPlatformHandler();
            app.UseCors("OpenBookAPI");
            app.UseSwagger();
            app.UseSwaggerUi();
            //app.UseHttpCache();

            app.UseJwtBearerAuthentication(options =>
            {
                options.Audience = Configuration["Auth:ClientId"];
                options.Authority = Configuration["Auth:Domain"];
                options.AuthenticationScheme = "Automatic";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true
                };
            });
            //should go at the end
            app.UseMvc();
            loggerFactory.AddSerilog();
        }
    }
}
