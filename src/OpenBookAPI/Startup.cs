using System;
using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;


namespace OpenBookAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //CORS -- temporary currently allow anyone to connect
            var OpenBookAPIcors = new Microsoft.AspNet.Cors.Core.CorsPolicy();
            OpenBookAPIcors.Headers.Add("*");
            OpenBookAPIcors.Origins.Add("*");
            OpenBookAPIcors.Methods.Add("*");
            OpenBookAPIcors.SupportsCredentials = true;
            services.ConfigureCors(cors => cors.AddPolicy("OpenBookAPI", OpenBookAPIcors));

            //Dependancy Injection
            Modules.Register(services);

            //Swagger
            //services.AddSwagger();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
            //app.UseSwagger();
            //app.UseSwaggerUi();
        }
    }
}
