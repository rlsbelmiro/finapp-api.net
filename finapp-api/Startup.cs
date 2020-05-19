using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinAppApi.Components.Business;
using FinAppApi.Components.Conection;
using FinAppApi.Components.Repositories;
using FinAppApi.Components.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace finapp_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<FinAppContext>(options =>
                options.UseMySql(connection)
            );
            services.AddCors();
            services.AddControllers();

            //Security
            var key = Encoding.ASCII.GetBytes(Settings.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Database
            services.AddScoped<FinAppContext, FinAppContext>();

            //BusinessObject
            services.AddTransient<ClientBusiness, ClientBusiness>();
            services.AddTransient<UserBusiness, UserBusiness>();
            services.AddTransient<DocumentBusiness, DocumentBusiness>();
            services.AddTransient<WalletBusiness, WalletBusiness>();
            services.AddTransient<CreditCardBusiness, CreditCardBusiness>();
            services.AddTransient<CategoryBusiness, CategoryBusiness>();
            //Repositories
            services.AddTransient<ClientRepository, ClientRepository>();
            services.AddTransient<UserRepository, UserRepository>();
            services.AddTransient<WalletRepository, WalletRepository>();
            services.AddTransient<CreditCardRepository, CreditCardRepository>();
            services.AddTransient<CategoryRepository, CategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
