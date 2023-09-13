using System.Text.Json;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using POC.Processos.Utils;

namespace POC.Processos.SQSService;

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
        GetConfiguration());
    
    private AmazonSQSConfig GetConfiguration() => new ()
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(_config.Region()),
        ServiceURL     = ServiceUrl
    };
    
    public async Task<RequestDTO?> ReceiveIntegrationRequest()
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl            = IntegrationQueueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds     = 10
        };
        
        var result = await _client.ReceiveMessageAsync(request);

        if (result.Messages is { Count: 0 }) return null;

        return new RequestDTO(
            result.Messages.First().Body, 
            result.Messages.First().ReceiptHandle);
    }

    public Task DeleteMessage(string messageId)
        => _client.DeleteMessageAsync(IntegrationQueueUrl, messageId);

    public async Task SendSuccessEvent(string number)
        => await _client.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl    = SuccessQueueUrl,
            MessageBody = number
        });

    public async Task SendFailureEvent(string? number, string message) 
        => await _client.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl    = ErrorQueueUrl,
            MessageBody = JsonSerializer.Serialize(new { number, message })
        });
}