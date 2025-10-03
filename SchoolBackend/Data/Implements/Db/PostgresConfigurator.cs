using Data.Infrastructure.Interceptors;
using System;
using Data.Interfaces.Factory;
using Entity.Context.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Implements.Db
{
    public class PostgresConfigurator : IDbEngineConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration, string connectionName)
        {


            services.AddDbContext<AplicationDbContext>((servicesProvider, options) =>
            {
                var interceptor = servicesProvider.GetRequiredService<LogginDbCommandsInterceptor>();

                options.UseNpgsql(configuration.GetConnectionString(connectionName));

                options.AddInterceptors(interceptor);
            });

        }
    }
}
