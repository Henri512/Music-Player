using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicLibrary.Domain.Services;
using MusicPlaSyer.Model.Services;
using MusicPlayer.Model.Repositories;
using MusicPlayer.Model.Services;
using MusicPlayer.Models;
using MusicPlayer.Repositories;
using MusicPlayer.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MusicPlayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // send back a ISO date
                    var settings = options.SerializerSettings;
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    // dont mess with case of properties
                    var resolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
                    resolver.NamingStrategy = new CamelCaseNamingStrategy();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MusicPlayerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MusicPlayerCN")));

            services.AddTransient<ISongInfoRepository, SongInfoRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<ISongInfoService, SongInfoService>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<MusicPlayerContext>();
            services.AddScoped<DbContext, MusicPlayerContext>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
