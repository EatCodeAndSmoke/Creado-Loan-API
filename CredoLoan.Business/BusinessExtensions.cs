using AutoMapper;
using CredoLoan.Business.Models;
using CredoLoan.DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CredoLoan.Business {
    public static class BusinessExtensions {

        public static IServiceCollection AddCredoLoanServices(this IServiceCollection services) {
            services.AddDalServices();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ClientReadModel)));
            return services;
        }
    }
}
