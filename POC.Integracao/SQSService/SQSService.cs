using System.Text.Json;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using POC.Integracao.Utils;

namespace POC.Integracao.SQSService;

public class SQSService : ISQSService
{
    private readonly IConfiguration _config;
    private readonly IAmazonSQS _client;
    
    private string ServiceUrl 
        => $"https://sqs.{_config.Region()}.amazonaws.com";

    private string IntegrationQueueUrl
        => $"{ServiceUrl}/{_config.Account()}/{_config.IntegrationQueue()}";
    
    private string SuccessQueueUrl
        => $"{ServiceUrl}/{_config.Account()}/{_config.SuccessQueue()}";
    
    private string ErrorQueueUrl
        => $"{ServiceUrl}/{_config.Account()}/{_config.ErrorQueue()}";
    
    public SQSService(IConfiguration config)
    {
        _config = config;
        _client = NewClient();
    }

    private AmazonSQSClient NewClient() => new(
        _config.AccessKey(),
        _config.SecretKey(),
        GetConfig());

    private AmazonSQSConfig GetConfig() => new ()
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Region()),
        ServiceURL     = ServiceUrl
    };

    public async Task RequestNewIntegration(string number) => await _client
        .SendMessageAsync(new SendMessageRequest
        {
            QueueUrl    = IntegrationQueueUrl,
            MessageBody = number
        });

    public async Task<string> ReceiveSuccessRequest()
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl            = SuccessQueueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds     = 10
        };
        
        var result = await _client.ReceiveMessageAsync(request);

        return result.Messages is { Count: 0 } 
            ? string.Empty 
            : result.Messages.First().Body;
    }

    public async Task<FailureDTO?> ReceiveFailureRequest()
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl            = ErrorQueueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds     = 10
        };
        
        var result = await _client.ReceiveMessageAsync(request);

        return result.Messages is { Count: 0 }
            ? null
            : JsonSerializer.Deserialize<FailureDTO>(result.Messages.First().Body);
    }
}