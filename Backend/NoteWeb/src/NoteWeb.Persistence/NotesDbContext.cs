using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NoteWeb.Persistence.Configurations;
using NoteWeb.Persistence.Entities;

namespace NoteWeb.Persistence;

public class NotesDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<NoteEntity> Notes { get; set; }

    public NotesDbContext(IConfiguration  configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}
