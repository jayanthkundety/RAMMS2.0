using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Business.ServiceProvider.Services;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMMS.Root.CustomInjection;
using RAMMS.Web.UI.Middlewares;
using RAMS.Repository;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Web.UI.Filters;

namespace RAMMS.Web.UI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {

            var webHostBuilder = new WebHostBuilder();
            var environment = webHostBuilder.GetSetting("environment");
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .AddEnvironmentVariables();
            Configuration = builder.Build();

            Serilog.Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // services.ConfigureDbContext(Configuration);
            //services.ConfigureServices();

            string con = Configuration.GetConnectionString("RAMMSDatabase");
            services.AddDbContext<RAMMSContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("RAMMSDatabase")), ServiceLifetime.Transient);
            services.AddDbContext<RAMSContext>(options =>
                       options.UseSqlServer(Configuration.GetConnectionString("RAMMSDatabase")), ServiceLifetime.Transient);
            var logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
               .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
               .WriteTo.Console()
               .CreateLogger();

            services.AddLogging(b =>
            {
                b.AddSerilog(logger, true);
            });
            services.AddSingleton(typeof(Serilog.ILogger), logger);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.InjectAppDependencies();

            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.InitAutoMapper();
            services.AddDirectoryBrowser();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddJWTTokenAuthentication(Configuration, false);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => { o.LoginPath = new PathString("/Signin"); o.AccessDeniedPath = "/error/403"; });

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(CustomExceptionFilter));
            });
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var context = app.GetContext();
            //var mssqlConnectionString = context.Configuration.GetConnectionString("RAMMSDatabase");

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCustomException();

            //app.UseExceptionHandler("/Error");
            //app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SignIn}/{action=Index}/{id?}");
            });
        }
    }
}
