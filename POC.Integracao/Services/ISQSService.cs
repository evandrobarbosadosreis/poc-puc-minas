namespace POC.Integracao.Services;

public interface ISQSService
{
    Task RequestNewIntegration(string number);
}