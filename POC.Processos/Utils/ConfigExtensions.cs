namespace POC.Processos.Utils;

public static class ConfigExtensions
{
    public static string AccessKey(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_ACCESS_KEY") ?? string.Empty;
    }
    
    public static string SecretKey(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_SECRET_KEY") ?? string.Empty;
    }
    
    public static string Region(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_REGION") ?? string.Empty;
    }
    
    public static string ConsumerUrl(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_QUEUE_CONSUMER") ?? string.Empty;
    }
    
    public static string SuccessUrl(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_QUEUE_SUCCESS") ?? string.Empty;
    }
    
    public static string ErrorUrl(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_QUEUE_ERROR") ?? string.Empty;
    }
}