namespace POC.Integracao.Extensions;

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
    
    public static string ServiceUrl(this IConfiguration source)
    {
        return source.GetValue<string>("AWS_QUEUE_URL") ?? string.Empty;
    }
}