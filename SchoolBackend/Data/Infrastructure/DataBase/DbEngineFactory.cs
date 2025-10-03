using Data.Implements.Db;
using Data.Interfaces.Factory;

namespace Data.Infrastructure.DataBase
{
    public static class DbEngineFactory
    {
        public static IDbEngineConfigurator GetConfigurator(string engine)
        {
            return engine switch
            {
                "PgAdmin" => new PostgresConfigurator(),
                "PgAdminLog" => new PostgresLogConfigurator(),
                "MySQL" => new MySqlConfigurator(),
                "SqlServer" => new SqlServerConfigurator(),
                _ => throw new InvalidOperationException($"Motor no soportado: {engine}")
            };
        }
    }
}
