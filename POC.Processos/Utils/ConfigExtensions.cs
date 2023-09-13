namespace POC.Processos.Utils;

public static class ConfigExtensions
{
    public static string AccessKey(this IConfiguration source) 
        => source.GetValue<string>("AWS_ACCESS_KEY") ?? string.Empty;
    public static string SecretKey(this IConfiguration source) 
        => source.GetValue<string>("AWS_SECRET_KEY") ?? string.Empty;
    public static string Region(this IConfiguration source)
        => source.GetValue<string>("AWS_REGION") ?? string.Empty;
    public static string IntegrationQueue(this IConfiguration source) 
        => source.GetValue<string>("AWS_INTEGRATION_QUEUE") ?? string.Empty;
    public static string Account(this IConfiguration source) 
        => source.GetValue<string>("AWS_ACCOUNT") ?? string.Empty;
    public static string SuccessQueue(this IConfiguration source) 
        => source.GetValue<string>("AWS_SUCCESS_QUEUE") ?? string.Empty;
    public static string ErrorQueue(this IConfiguration source) 
        => source.GetValue<string>("AWS_ERROR_QUEUE") ?? string.Empty;
}