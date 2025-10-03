using Data.Interfaces.Factory;
using Entity.Context.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Implements.Db
{
    public class MySqlConfigurator : IDbEngineConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString(connectionName)));
        }
    }
}
