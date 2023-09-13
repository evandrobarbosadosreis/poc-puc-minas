using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using POC.Integracao.Extensions;

namespace POC.Integracao.SQSService;

public class SQSService : ISQSService
{
    private readonly IConfiguration _config;

    public SQSService(IConfiguration config)
    {
        _config = config;
    }
    
    public async Task RequestNewIntegration(string number) => await NewClient()
        .SendMessageAsync(new SendMessageRequest
            {
                QueueUrl    = _config.ServiceUrl(),
                MessageBody = number
            });

    private AmazonSQSClient NewClient() => new(
        _config.AccessKey(),
        _config.SecretKey(),
        GetConfig());

    private AmazonSQSConfig GetConfig() => new ()
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Region()),
        ServiceURL = $"https://sqs.{_config.Region()}.amazonaws.com"
    };
}