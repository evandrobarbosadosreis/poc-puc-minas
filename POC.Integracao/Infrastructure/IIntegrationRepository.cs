using POC.Integracao.Entities;

namespace POC.Integracao.Infrastructure;

public interface IIntegrationRepository
{
    public Task<Integration> Create(string number);
    public Task<List<Integration>> GetBy(string number);
}