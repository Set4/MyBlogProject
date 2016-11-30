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
using WebView.Model;

namespace MyBlog
{
    public class GlobalSetting
    {
        public int MAX_COUNT_POSTS { get; set; }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json") // подключаем файл конфигурации
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //var connection = @"=./SQLEXPRESS; Database = blogappdb; Trusted_Connection = True;";
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=blogappdb;Trusted_Connection=True;";
            //services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            /*

            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString(@"Server =./SQLEXPRESS; Database = blogappdb; Trusted_Connection = True;");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(connection));

            */
            // Настройка параметров и DI
            services.AddOptions();

            // создание объекта GlobalSetting по ключам из конфигурации
            services.Configure<GlobalSetting>(Configuration);


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
            });
        }
    }
}
