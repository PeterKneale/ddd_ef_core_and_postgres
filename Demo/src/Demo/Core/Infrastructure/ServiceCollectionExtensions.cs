using System.Data;
using System.Reflection;
using Dapper;
using Demo.Core.Application;
using Demo.Core.Infrastructure.Repository;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Demo.Core.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Db");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string missing");
        }
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging(true);
            options.UseSnakeCaseNamingConvention();
        });

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
        
        return services;
    }
}