using System.IdentityModel.Tokens;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Swashbuckle.Swagger;
using OpenBookAPI.HttpCache;
using Serilog.Sinks.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using OpenBookAPI.SignalR.Hubs;
using System;

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
            

            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });
            
            
            
            
            services.AddInstance<Serilog.ILogger>(Log.Logger);
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app,ILoggerFactory loggerFactory, IHostingEnvironment env,IRuntimeEnvironment runtimeEnv,IConnectionManager connectionManager)
        {
            app.UseStaticFiles();
            app.UseIISPlatformHandler();
            app.UseCors("OpenBookAPI");
            app.UseSwagger();
            app.UseSwaggerUi();
            app.UseSignalR();
            var hubContext = connectionManager.GetHubContext<OpenBookAPI.SignalR.Hubs.LogHub>();
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
            
            var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.Trace()
                .WriteTo.Console()
                .WriteTo.Sink(new SignalRSink(hubContext,10,TimeSpan.FromSeconds(10)));
                                    
            if(runtimeEnv.RuntimeType == "Windows")
            {
                loggerConfiguration.WriteTo.AzureDocumentDB(new System.Uri("https://openbook.documents.azure.com:443/"),"j4Hzifc5HL6z4uNt152t6ECrI5J7peGpSJDlwfkEzn5Vs94pAxf71N3sw3iQS6YneXC0CvxA+MdQjP/GKVbo6A==");
            }
            
            Log.Logger = loggerConfiguration.CreateLogger();
            Log.Logger.Debug($"environment = {runtimeEnv.RuntimeType}");
            
            //should go at the end
            app.UseMvc();
            loggerFactory.AddSerilog();
        }
    }
}
