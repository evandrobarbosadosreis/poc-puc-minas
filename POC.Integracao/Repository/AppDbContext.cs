using Microsoft.EntityFrameworkCore;
using POC.Integracao.Entities;

namespace POC.Integracao.Repository;

public class AppDbContext : DbContext
{
    public DbSet<Integration> Integrations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}