using POC.Integracao.Entities;

namespace POC.Integracao.Repository;

public interface IIntegrationRepository
{
    Task<Integration> Create(string number);
    Task<List<Integration>> GetBy(string number);
    Task<Integration?> GetFirstBy(string number);
    Task Update(Integration integration);
}