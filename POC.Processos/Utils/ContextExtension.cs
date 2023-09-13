using System.Linq.Expressions;

namespace POC.Processos.Utils;

public static class ContextExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, string param, Expression<Func<T, bool>> predicate)
    {
        if (string.IsNullOrWhiteSpace(param))
        {
            return source;
        }
        
        return source.Where(predicate);
    }
}