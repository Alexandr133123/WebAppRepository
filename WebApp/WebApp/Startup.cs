using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Repository;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.BusinessLogicLayer.Services;
using WebApp.PresentationLayer.MapperConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using WebApp.PresentationLayer.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                AuthOptions authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>(); 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey()
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("Cookie"))
                        {
                            context.Token = context.Request.Cookies["Cookie"];
                        }
                        return Task.CompletedTask;
                    }
                };                      
            
            });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = false;
            });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                       options.UseSqlServer(connection));
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                    .AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProcedureManager, ProcedureManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IAuthService,AuthService>();
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
            app.UseRouting();


            app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

    
        }
    }
}
