namespace POC.Integracao.SQSService;

public interface ISQSService
{
    Task RequestNewIntegration(string number);
}