using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CredoLoan.DAL {
    public static class DalExtensions {

        public static IServiceCollection AddDalServices(this IServiceCollection services) {
            services.AddDbContext<CredoLoanDbContext>(builder => {
                builder.UseInMemoryDatabase("InMemoryDb");
            });
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
