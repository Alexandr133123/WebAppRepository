using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Repository;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.BusinessLogicLayer.Services;
using WebApp.PresentationLayer.MapperConfig;
using Microsoft.EntityFrameworkCore.Proxies;
using AutoMapper;
using WebApp.PresentationLayer.DTO;

namespace WebApp
{
    public class Startup
    {
     
    

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                       options.UseSqlServer(connection));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProcedureManager, ProcedureManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IDownloadService, DownloadService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IMapper>((s) => MapperConfigManager.GetConfig().CreateMapper());
            services.AddControllers();
            services.AddCors();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseStaticFiles();
     
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

    
        }
    }
}
