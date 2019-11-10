using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.Data;
using MusicPlayer.Infrastructure.Albums;
using MusicPlayer.Infrastructure.SongInfos;
using MusicPlayer.Utilities.Helpers;

namespace MusicPlayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.MaxDepth = 4;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<MusicPlayerContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("ConnectionStrings__MusicPlayerCN"),
                        x => x.MigrationsAssembly(typeof(MusicPlayerContext).Assembly.FullName)));


            services.AddTransient<ISongInfoRepository, SongInfoRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<ISongInfoService, SongInfoService>();

            services.AddTransient<IExpressionHelper, ExpressionHelper>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<MusicPlayerContext>();

            services.AddScoped<DbContext, MusicPlayerContext>();

            services.AddSingleton(Configuration);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors("CorsPolicy");
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

            Bootstrap();
        }

        private async void Bootstrap()
        {
            var options = new BrowserWindowOptions
            {
                WebPreferences = new WebPreferences
                {
                    WebSecurity = false
                }
            };

            await Electron.WindowManager.CreateWindowAsync(options);
        }
    }
}
