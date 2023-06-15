using FluentMigrator.Runner;
using HardwareStore.Data.Migrations;
using HardwareStore.Data.Read;
using HardwareStore.Data.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareStore.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDatabaseSources(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddTransient(_ => new StoreReadonlyContext(connectionString));

        services.AddDbContext<HardwareStoreContext>(options =>
        {
            options.UseNpgsql(connectionString,
                postgresOptions =>
                {
                    postgresOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                });
        });

        return services;
    }
    
    public static IServiceCollection AddMigrations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(r => r
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection")!)
                .ScanIn(typeof(InitialMigration).Assembly)
                .For.Migrations());

        services.AddHostedService<MigrationHostedService>();
        return services;
    }
}