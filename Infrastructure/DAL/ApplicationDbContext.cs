using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;


    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

// RUN THIS COMMAND IN THE ROOT FOLDER OF INFRASTRUCTURE PROJECT TO CREATE MIGRATIONS
//! dotnet ef migrations add InitialCreate  --startup-project ../Presentation --output-dir ./DAL/Migrations
// RUN THIS COMMAND IN THE ROOT FOLDER OF INFRASTRUCTURE PROJECT TO UPDATE DATABASE
//! dotnet ef database update --startup-project ../Presentation --output-dir ./DAL/Migrations