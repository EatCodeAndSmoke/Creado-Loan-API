using CredoLoan.Api.Filters;
using CredoLoan.Api.Middlewares;
using CredoLoan.Api.Tokens;
using CredoLoan.Business;
using CredoLoan.Business.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CredoLoan.Api {
    public class Startup {

        private readonly IConfiguration _config;

        public Startup(IConfiguration config) {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddCredoLoanServices();

            var tokenParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"])),
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = tokenParameters;
                });

            services.AddAuthorization();
            services.AddMvc(opt => opt.Filters.Add(typeof(ValidateModelAttribute)))
                .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<ClientReadModel>());

            services.AddScoped<IJwtTokenManager, JwtTokenManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {

            loggerFactory.AddFile("Logs/log-{Date}.txt");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseRequestResponseLoggingMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
