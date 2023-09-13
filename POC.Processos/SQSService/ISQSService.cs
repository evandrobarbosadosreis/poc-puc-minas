namespace POC.Processos.SQSService;

public interface ISQSService
{
    Task<RequestDTO?> ReceiveIntegrationRequest();
    Task DeleteMessage(string messageId);
    Task SendFailureEvent(string? number, string message);
    Task SendSuccessEvent(string number);
}