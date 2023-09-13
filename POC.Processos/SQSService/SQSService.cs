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
        ServiceURL = $"https://sqs.{_config.Region()}.amazonaws.com"
    };

    public async Task<RequestDTO?> ReceiveIntegrationRequest()
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl            = _config.ConsumerUrl(),
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
        => _client.DeleteMessageAsync(
            _config.ConsumerUrl(), 
            messageId);

    public async Task SendSuccessEvent(string number)
        => await _client.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl    = _config.SuccessUrl(),
            MessageBody = number
        });

    public async Task SendFailureEvent(string? number, string message) 
        => await _client.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl    = _config.ErrorUrl(),
            MessageBody = JsonSerializer.Serialize(new { number, message })
        });
}