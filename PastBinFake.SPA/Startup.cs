﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PastBinFake.SPA.DataAccessLayer;
using PastBinFake.SPA.DataAccessLayer.Memory;
using PastBinFake.SPA.DomainLogic;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;
using PastBinFake.SPA.DomainLogic.Models;
using PastBinFake.SPA.DomainLogic.UrlGenerators;
using PastBinFake.SPA.ViewModels;
using Serilog;
using Serilog.Sinks.RollingFile;

namespace PastBinFake.SPA
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
            
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.RollingFile(".\\logs")
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            Log.Logger.Debug($"Logger success created");
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            DependencyInjectionConfigure.Configure(services);
            
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));
            //services.AddLogging();
            ConfigureMapper(services);
        }

        private void ConfigureMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                MapperConfigure.Configure(cfg);
                DomainLogic.MapperConfigure.Configure(cfg);
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddSerilog();
//            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
//            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{*pathInfo}",
                    defaults: new {controller = "Home", action = "Index"});
            });
            
        }
    }
}
