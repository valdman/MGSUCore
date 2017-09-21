using System;
using MGSUBackend.Authentification;
using MGSUCore.Authentification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Common.Entities;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpOverrides;

namespace MGSUCore
{
    public class Startup
    {
        private readonly DeploySettings _deploySettings;
        public Startup(IHostingEnvironment env, IOptions<DeploySettings> deploySettings)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
              //.AddJsonFile("appsettings.json", optional: fale, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            _deploySettings = deploySettings.Value;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.  
            //services.AddApplicationInsightsTelemetry(Configuration);  

            // Add framework services.
            services.AddMvc();

            services.AddCors(  
            options => options.AddPolicy("AllowCors",  
                builder => {  
                    builder 
                    .WithOrigins("http://185.189.14.156")
                    .WithOrigins("http://localhost:3000")
                    //.WithOrigins(_deploySettings.FrontendAddress) //AllowSpecificOrigins;  

                    //.WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;  
                        //.AllowAnyOrigin() //AllowAllOrigins;  
                        //.WithMethods("GET") //AllowSpecificMethods;  
                        //.WithMethods("GET", "PUT") //AllowSpecificMethods;  
                        //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;  
                        .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                        //.AllowAnyMethod() //AllowAllMethods;  
                        //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
                        .AllowAnyHeader()
                        .AllowCredentials();
                    })  
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Admin",
                    policyBuilder => policyBuilder.AddRequirements(
                        new IsInRole(UserRole.Admin)));
                options.AddPolicy(
                    "User",
                    policyBuilder => policyBuilder.AddRequirements(
                        new IsAuthentificated()));
            });

            //Add DI starter
            new Bootstraper(services, Configuration).Configure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {       
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            
			//todo: use cookie auth middleware
			app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "WebCookieAuthMiddleware",
                //todo: remove magic 302 bug
                LoginPath = PathString.Empty,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            //CORS
            app.UseCors("AllowCors");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc();
            
        }
    }
}

