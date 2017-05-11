using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGSUBackend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace MGSUCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            
            //Add DI starter
            var container = new Bootstraper().Configure();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //var container = app.
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //// Add application presentation components:
            //container.RegisterMvcControllers(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //todo: use cookie auth middleware

            app.UseMvc();
        }
    }
}
