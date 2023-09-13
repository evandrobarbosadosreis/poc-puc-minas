using Microsoft.EntityFrameworkCore;
using POC.Processos.Entities;

namespace POC.Processos.Repository;

public class AppDbContext : DbContext
{
    public DbSet<Process> Process { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}