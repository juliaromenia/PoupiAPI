using Microsoft.EntityFrameworkCore;

namespace PoupiAPI.Data;

public class PoupiAppDbContext : DbContext
{
    public PoupiAppDbContext(DbContextOptions<PoupiAppDbContext> options) : base(options)
    {
    }

    public DbSet<Model.Usuario> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model.Usuario>().ToTable("Usuarios");
    }
}