using FluentMigrator.Runner;
using Polly;

namespace Demo.Core.Infrastructure;

public static class ServiceProviderExtensions
{
    public static IServiceProvider ApplyDatabaseMigrations(this IServiceProvider app, bool reset = false)
    {
        var RetryInterval = 5; // seconds
        var RetryAttempts = 1;
        var logs = app.GetRequiredService<ILogger<IMigrationRunner>>();

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                RetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(RetryInterval),
                (exception, timeSpan, attempt, context) =>
                    logs.LogWarning($"Attempt {attempt} of {RetryAttempts} failed with exception {exception.Message}. Delaying {timeSpan.TotalMilliseconds}ms"));

        policy.Execute(() =>
        {
            using var scope = app.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            if (reset)
            {
                runner.MigrateDown(0);
            }
            runner.MigrateUp();
        });

        return app;
    }
}