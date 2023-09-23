using POC.Integracao.Repository;
using POC.Integracao.SQSService;

namespace POC.Integracao.Consumer;

public class SuccessSQSConsumer : BackgroundService
{
    private readonly IServiceProvider _provider;

    public SuccessSQSConsumer(IServiceProvider provider)
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
            var number = await service.ReceiveSuccessRequest();

            if (number is "") continue;
            
            var integration = await repository.GetFirstBy(number);

            if (integration is null) continue;
            
            integration.MarkAsSuccess();

            await repository.Update(integration);
        }
    }
}