using POC.Processos.Integration;
using POC.Processos.Repository;
using POC.Processos.SQSService;

namespace POC.Processos.Consumer;

public class CommandSQSConsumer : BackgroundService
{
    private readonly IServiceProvider _provider;

    public CommandSQSConsumer(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _provider.CreateScope();
        
        var service    = scope.ServiceProvider.GetRequiredService<ISQSService>();
        var repository = scope.ServiceProvider.GetRequiredService<IProcessRepository>();
        var tribunal   = scope.ServiceProvider.GetRequiredService<ITribunalApi>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await service.ReceiveIntegrationRequest();

            if (request is null) continue;
            
            try
            {
                var process = await tribunal.GetProcessData(request.Number);
                await repository.CreateOrReplace(process, stoppingToken);
                await service.SendSuccessEvent(request.Number);
                await service.DeleteMessage(request.MessageId);
            }
            catch (Exception ex)
            {
                await service.SendFailureEvent(request.Number, ex.Message);
            }
        }
    }
}