using System.Reflection;
using Demo.Core.Application.Behaviours;
using FluentValidation;
using MediatR.Pipeline;

namespace Demo.Core.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidationBehaviour<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionalBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        return services;
    }
}