using Demo.Core.Application;
using Demo.Core.Infrastructure;
using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.IntegrationTests;

public class ServicesFixture : IDisposable, ITestOutputHelperAccessor
{
    private readonly ServiceProvider _provider;

    public ServicesFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json")
            .Build();
        
        _provider = new ServiceCollection()
            .AddApplication()
            .AddInfrastructure(configuration)
            .AddLogging(builder => builder.AddXUnit(this))
            .BuildServiceProvider();
    }

    public IServiceProvider ServiceProvider => _provider;
    
    public ITestOutputHelper? OutputHelper { get; set; }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        await using var scope = _provider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(request);
    }
    
    public void Dispose()
    {
        _provider.Dispose();
    }
}
