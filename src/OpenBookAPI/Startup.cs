﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Notifications;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Authentication.Cookies;

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

            //Configure Auth policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("registeredOnly", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("adminOnly", policy =>
                {
                    policy.RequireClaim("role", "admin");
                });
            });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.UseStaticFiles();
            //app.UseSwagger();
            //app.UseSwaggerUi();
            logger.AddConsole();
            app.UseOpenIdConnectAuthentication(options =>
            {
                options.Authority = "https://accounts.google.com/";
                options.ClientId = "336092105680-uattl87g384j7n5ibfid4v10c7pcerkp.apps.googleusercontent.com";
                options.ClientSecret = "PaZLmNwOCY_N-HUZKIEStabp";
            });

            //should go at the end
            app.UseMvc();
        }
    }
}
