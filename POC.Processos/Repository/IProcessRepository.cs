using POC.Processos.Entities;
using POC.Processos.Integration;

namespace POC.Processos.Repository;

public interface IProcessRepository
{
    public Task<Process> Create(ProcessDTO source, CancellationToken cancellation);
    public Task<List<Process>> GetBy(string number);
}