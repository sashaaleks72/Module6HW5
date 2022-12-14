using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Module6HW5.DB;
using Module6HW5.Interfaces;
using Module6HW5.Providers;
using Module6HW5.Services;

namespace Module6HW5
{
    public class Startup
    {
        private readonly string _myAllowSpecificOrigins;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"];
            var port = Configuration["DBPort"];
            var user = Configuration["DBUser"];
            var pass = Configuration["DBPassword"];
            var database = Configuration["DBName"];

            string connectionString = string.Empty;

            if (server != null)
                connectionString = $"Server={server},{port};Initial Catalog={database};User ID={user};Password={pass}";
            else connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllers();
            services.AddTransient<ITeapotDataProvider, TeapotDataProvider>();
            services.AddTransient<ITeapotService, TeapotService>();
            services.AddTransient<IAccountDataProvider, AccountDataProvider>();
            services.AddTransient<IRoleDataProvider, RoleDataProvider>();
            services.AddTransient<IAccountService, AccountService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Module6HW1", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Module6HW1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_myAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
