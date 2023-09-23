using Microsoft.EntityFrameworkCore;
using POC.Integracao.Entities;
using POC.Integracao.Utils;

namespace POC.Integracao.Repository;

public class IntegrationRepository : IIntegrationRepository
{
    private readonly AppDbContext _dbContext;

    public IntegrationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Integration> Create(string number)
    {        
        var result = new Integration(number);

        _dbContext.Add(result);
        
        await _dbContext.SaveChangesAsync();

        return result;
    }

    public Task<List<Integration>> GetBy(string number)
    {
        return _dbContext
            .Set<Integration>()
            .WhereIf(number, x => x.Number.Contains(number))
            .ToListAsync();
    }

    public Task<Integration?> GetFirstBy(string number)
    {
        if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException();
        
        return _dbContext
            .Set<Integration>()
            .FirstOrDefaultAsync(x => x.Number.Equals(number));
    }

    public async Task Update(Integration integration)
    {
        _dbContext.Update(integration);
        await _dbContext.SaveChangesAsync();
    }
}