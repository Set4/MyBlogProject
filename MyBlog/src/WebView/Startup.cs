using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebBlog.Model;
using WebBlog.DataAccess;
using Pioneer.Pagination;

namespace MyBlog
{
    public class GlobalSetting
    {
        public int MAX_VIEW_WEB_COUNT_POSTS { get; set; }
        public int MAX_VIEW_API_COUNT_POSTS { get; set; }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configurationSection = Configuration.GetSection("DataBaseConnection");
            DataAccess.Config.DBSource = configurationSection.GetValue<string>("DBSource");
            DataAccess.Config.DBUserName = configurationSection.GetValue<string>("DBUserName");
            DataAccess.Config.DBPassword = configurationSection.GetValue<string>("DBPassword");
            DataAccess.Config.DBInitialCatalog = configurationSection.GetValue<string>("DBInitialCatalog");

            // Настройка параметров и DI
            services.AddOptions();

            // создание объекта GlobalSetting по ключам из конфигурации
            //services.Configure<GlobalSetting>(Configuration);

            services.Configure<GlobalSetting>(Configuration.GetSection("PostSettings"));

            //add pagination nuget package
            services.AddTransient<IPaginatedMetaService, PaginatedMetaService>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    "start", "", new { controller = "Post", action = "GetPostCollection" });
                routes.MapRoute(
                   "posts", "page{page}/", new { controller = "Post", action = "GetPostCollection", page= @"^[1-9]\d*$" });
            });
        }
    }
}
