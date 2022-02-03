using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RAMMS.Business.ServiceProvider;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMMS.Root.CustomInjection;
using RAMS.Repository;
using Serilog;
using RAMMS.Business.ServiceProvider.Services;
namespace RAMMS.WebAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
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

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RAMMS API Service" });
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddResponseCaching()
           .AddSession(options =>
           {
               options.IdleTimeout = TimeSpan.FromMinutes(800);
               options.Cookie.Name = "RAMMS";

           });
            // camelCase result type issue 
            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            string con = Configuration.GetConnectionString("RAMMSDatabase");
            services.AddDbContext<RAMMSContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("RAMMSDatabase")));
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

            services.AddRazorPages();

            services.AddControllersWithViews();
            services.AddJWTTokenAuthentication(Configuration,true);
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RAMMS API");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseCors("MyPolicy");
            app.UseResponseCaching();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRewriter(new RewriteOptions().AddRedirect("/*", "/swagger"));


            
            

            

        }
    }
}
