using POC.Integracao.Repository;
using POC.Integracao.SQSService;

namespace POC.Integracao.Consumer;

public class FailureSQSConsumer : BackgroundService
{
    private readonly IServiceProvider _provider;

    public FailureSQSConsumer(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        
        var service    = scope.ServiceProvider.GetRequiredService<ISQSService>();
        var repository = scope.ServiceProvider.GetRequiredService<IIntegrationRepository>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await service.ReceiveFailureRequest();

            if (request is null) continue;
            
            var integration = await repository.GetFirstBy(request.number);
            
            if (integration is null) continue;

            integration.MarkAsFailed();

            await repository.Update(integration);
        }
    }
}