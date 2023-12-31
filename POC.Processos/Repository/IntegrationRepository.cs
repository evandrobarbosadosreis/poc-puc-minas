using Microsoft.EntityFrameworkCore;
using POC.Processos.Entities;
using POC.Processos.Integration;
using POC.Processos.Utils;

namespace POC.Processos.Repository;

public class ProcessRepository : IProcessRepository
{
    private readonly AppDbContext _dbContext;

    public ProcessRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Process> CreateOrReplace(ProcessDTO source, CancellationToken cancellation)
    {
        var current = await _dbContext
            .Set<Process>()
            .FirstOrDefaultAsync(x => x.Number == source.Number, cancellation);

        if (current is not null)
        {
            _dbContext.Remove(current);
        }
        
        var process = new Process(
            source.Number, 
            source.Date);
        
        process.Add(source
            .Parts
            .Select(x => new Part(x.Name, x.Kind))
            .ToArray());

        _dbContext.Add(process);
        
        await _dbContext.SaveChangesAsync(cancellation);

        return process;
    }

    public Task<List<Process>> GetBy(string number)
    {
        return _dbContext
            .Set<Process>()
            .Include(x => x.Parts)
            .WhereIf(number, x => x.Number.Contains(number))
            .ToListAsync();
    }
}