using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Diagnostics;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using Newtonsoft.Json.Serialization;

using SampleApp.EF;

namespace SampleApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        
        public Startup()
        {
            Configuration = LoadConfiguration();
        }
        
        public static IConfiguration LoadConfiguration()
        {
            return
                new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.Configure<MvcOptions>(options =>
            {
                var settings = 
                    options.OutputFormatters
                           .Select(f => f as JsonOutputFormatter)
                           .First(f => f != null)
                           .SerializerSettings;
                
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            
            services.AddSingleton(serviceProvider =>
            {
                return new MainContextFactory(Configuration);
            });
            
            services.AddScoped(serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<MainContextFactory>();
                
                return factory.Create();
            });
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage(ErrorPageOptions.ShowAll);
            
            //app.UseStatusPages();
            
            app.UseMvc();
            
            // wwwroot
            app.UseStaticFiles();
            
            // bower_components
            ServeDirectory(app, "bower_components", "/bower_components");
        }
        
        private static void ServeDirectory(IApplicationBuilder app, String directoryPath, String requestPath)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(currentDirectory, directoryPath);
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString(requestPath),
                FileProvider = new PhysicalFileProvider(fullPath)
            });
        }
    }
}
